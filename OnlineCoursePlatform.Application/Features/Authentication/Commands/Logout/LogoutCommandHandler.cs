using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RevokeToken;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.Logout
{
    public class LogoutCommandHandler(UserManager<User> _userManager, IMediator _mediator, ICookieService _cookieService, 
        ILogger<LogoutCommandHandler> _logger) : IRequestHandler<LogoutCommand, bool>
    {
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = request.refreshToken;
            var userId = request.userId;
            var result = await _mediator.Send(new RevokeTokenCommand(refreshToken));
            _logger.LogInformation("Refreshtoken revoked.");
            var user = await _userManager.FindByIdAsync(userId);
            if (!result)
            {
                _logger.LogWarning("Failed to revoke token during logout.");
                return false;
            }
            user.IsActive = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("User logged out successfully.");
            _cookieService.RemoveFromCookies("refreshToken");
            _cookieService.RemoveFromCookies("userID");
            _cookieService.RemoveFromCookies("userName");
            return true;
        }
    }
}
