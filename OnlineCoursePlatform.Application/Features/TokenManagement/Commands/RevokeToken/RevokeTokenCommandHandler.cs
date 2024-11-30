using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler(UserManager<User> _userManager, ILogger<RevokeTokenCommandHandler> _logger)
        : IRequestHandler<RevokeTokenCommand, bool>
    {
        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.token;
            // Find user by refresh token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            // Return false if user not found
            if (user == null)
            {
                _logger.LogWarning("Token revocation failed: user not found.");
                return false;
            }
            // Get the refresh token
            var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == token);
            // Return false if token is inactive (already revoked or expired)
            if (!refreshToken.IsActive)
            {
                _logger.LogWarning($"Token revocation failed: token already inactive for user {user.UserName}");
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
    }
}
