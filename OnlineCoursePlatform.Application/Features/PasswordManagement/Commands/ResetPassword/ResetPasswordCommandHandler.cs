using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(UserManager<User> _userManager, ILogger<ResetPasswordCommandHandler> logger)
        : IRequestHandler<ResetPasswordCommand, string>
    {
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetPasswordDto = request.resetPasswordDto;
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null || user.IsDeleted)
            {
                logger.LogWarning("Email is incorrect");
                return "Email is incorrect!";
            }
                
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                logger.LogWarning("Invalid token!");
                return "Invalid token!";
            }

            logger.LogInformation("Your password has been reset successfully.");
            return "Your password has been reset successfully.";
        }
    }
}
