using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger, UserManager<User> _userManager)
        : IRequestHandler<ChangePasswordCommand, string>
    {
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = request.changePasswordDto;
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user == null || user.IsDeleted)
            {
                logger.LogWarning("Email is incorrect!");
                return "Email is incorrect!";
            }
                
            if (changePasswordDto.CurrentPassword.Equals(changePasswordDto.NewPassword))
            {
                logger.LogWarning("New and old password cannot be the same!");
                return "New and old password cannot be the same!";
            }
            

            var result = await _userManager.ChangePasswordAsync(user,
                changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                logger.LogError("An error occurred while changing the password!");
                return "An error occurred while changing the password!";
            }

            logger.LogInformation("Your password has been updated successfully.");
            return "Your password has been updated successfully.";
        }
    }
}
