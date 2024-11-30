using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineCoursePlatform.Application.Configurations;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.CreateToken
{
    public class CreateTokenCommandHandler: IRequestHandler<CreateTokenCommand, JwtSecurityToken>
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;
        private readonly ILogger<CreateTokenCommandHandler> _logger;
        public CreateTokenCommandHandler(UserManager<User> userManager, IOptions<JWT> jwt, ILogger<CreateTokenCommandHandler> logger)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _logger = logger;
        }
        public async Task<JwtSecurityToken> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = request.user;
            _logger.LogInformation("Creating JWT token for user {UserName} ({Email})", user.UserName, user.Email);
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
            _logger.LogInformation("JWT token successfully created for user {UserName} ({Email})", user.UserName, user.Email);
            return jwtSecurityToken;
        }
    }
}
