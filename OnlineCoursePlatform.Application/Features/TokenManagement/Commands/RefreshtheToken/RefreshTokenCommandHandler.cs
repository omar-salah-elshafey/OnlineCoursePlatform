using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.CreateToken;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.GenerateRefreshToken;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RefreshtheToken
{
    public class RefreshTokenCommandHandler(UserManager<User> _userManager, ILogger<RefreshTokenCommandHandler> _logger, 
        IMediator _mediator) : IRequestHandler<RefreshTokenCommand, AuthResponseModel>

    {
        public async Task<AuthResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.token;
            _logger.LogInformation("Attempting to refresh token for token: {Token}", token);
            var authResponseModel = new AuthResponseModel();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authResponseModel.IsAuthenticated = false;
                authResponseModel.Message = "Invalid Token!";
                _logger.LogWarning("Refresh token is invalid.");
                return authResponseModel;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                authResponseModel.IsAuthenticated = false;
                authResponseModel.Message = "Inactive Token!";
                _logger.LogWarning("Refresh token is inactive for user {UserName}.", user.UserName);
                return authResponseModel;
            }
            // Revoke current refresh token
            refreshToken.RevokedOn = DateTime.UtcNow.ToLocalTime();
            var newRefreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await _mediator.Send(new CreateTokenCommand(user));
            authResponseModel.IsAuthenticated = true;
            authResponseModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authResponseModel.ExpiresAt = jwtToken.ValidTo;
            authResponseModel.Email = user.Email;
            authResponseModel.Username = user.UserName;
            authResponseModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";
            authResponseModel.RefreshToken = newRefreshToken.Token;
            authResponseModel.RefreshTokenExpiresOn = newRefreshToken.ExpiresOn.ToLocalTime();
            _logger.LogInformation("Refresh token successfully generated for user {UserName}. New token: {Token}", 
                user.UserName, newRefreshToken.Token);
            return authResponseModel;
        }
    }
}
