using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.PasswordManagement.Dtos;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public class VerifyResetPasswordRequestCommandHandler(ILogger<VerifyResetPasswordRequestCommandHandler> logger,
        UserManager<User> _userManager) : IRequestHandler<VerifyResetPasswordRequestCommand, ResetPasswordResponseModel>
    {
        public async Task<ResetPasswordResponseModel> Handle(VerifyResetPasswordRequestCommand request, 
            CancellationToken cancellationToken)
        {
            var confirmEmailDto = request.ConfirmEmailDto;
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null || user.IsDeleted)
            {
                logger.LogWarning("User not found.");
                return new ResetPasswordResponseModel { IsRequestVerified = false, Message = "User not found." };
            }
                
            var result = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", confirmEmailDto.Token);
            if (!result)
            {
                logger.LogError("An Error Occured, or The Token is not valid!");
                return new ResetPasswordResponseModel { IsRequestVerified = false, Message = "Token is not valid!" };
            }

            logger.LogInformation("Your Password reset request is verified.");
            return new ResetPasswordResponseModel
            {
                IsRequestVerified = true,
                Message = "Your Password reset request is verified."
            };
        }
    }
}
