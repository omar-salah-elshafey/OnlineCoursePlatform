using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineCoursePlatform.Application.Configurations;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCoursePlatform.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;
        private readonly ILogger<TokenService> _logger;
        public TokenService(UserManager<User> userManager, IOptions<JWT> jwt, ILogger<TokenService> logger)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _logger = logger;
        }
        public async Task<JwtSecurityToken> CreateJwtTokenAsync(User user)
        {
            var userClaim = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }.Union(userClaim).Union(roleClaims);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey)),
                SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.ToLocalTime().AddMinutes(_jwt.Lifetime),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthResponseModel> RefreshTokenAsync(string token)
        {
            var authResponseModel = new AuthResponseModel();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authResponseModel.IsAuthenticated = false;
                authResponseModel.Message = "Invalid Token!";
                return authResponseModel;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                authResponseModel.IsAuthenticated = false;
                authResponseModel.Message = "Inactive Token!";
                return authResponseModel;
            }
            // Revoke current refresh token
            refreshToken.RevokedOn = DateTime.UtcNow.ToLocalTime();
            var newRefreshToken = await GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtTokenAsync(user);
            authResponseModel.IsAuthenticated = true;
            authResponseModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authResponseModel.ExpiresAt = jwtToken.ValidTo;
            authResponseModel.Email = user.Email;
            authResponseModel.Username = user.UserName;
            authResponseModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";
            authResponseModel.RefreshToken = newRefreshToken.Token;
            authResponseModel.RefreshTokenExpiresOn = newRefreshToken.ExpiresOn.ToLocalTime();
            return authResponseModel;
        }

        public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            // Find user by refresh token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            // Return false if user not found
            if (user == null)
            {
                _logger.LogInformation("Token revocation failed: user not found.");
                return false;
            }
            // Get the refresh token
            var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == token);
            // Return false if token is inactive (already revoked or expired)
            if (!refreshToken.IsActive)
            {
                _logger.LogInformation($"Token revocation failed: token already inactive for user {user.UserName}");
                return false;
            }
            // Revoke the refresh token
            refreshToken.RevokedOn = DateTime.UtcNow.ToLocalTime();
            // Update user with revoked token
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Token revoked successfully for user {user.UserName} at {DateTime.UtcNow.ToLocalTime()}");
                return true;
            }
            // Log and return false if update fails
            _logger.LogError($"Token revocation failed for user {user.UserName} due to update failure.");
            return false;
        }


        public async Task<RefreshToken> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                createdOn = DateTime.UtcNow.ToLocalTime(),
                ExpiresOn = DateTime.Now.AddDays(1),
                Token = Convert.ToBase64String(randomNumber)
            };
        }


    }
}
