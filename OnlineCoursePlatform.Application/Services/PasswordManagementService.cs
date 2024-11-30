using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Services
{
    public class PasswordManagementService : IPasswordManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions;

        public PasswordManagementService(UserManager<User> userManager, IEmailService emailService,
            IOptions<DataProtectionTokenProviderOptions> tokenProviderOptions)
        {
            _userManager = userManager;
            _emailService = emailService;
            _tokenProviderOptions = tokenProviderOptions;
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null || user.IsDeleted)
                return "Email is incorrect!";

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
                return "Invalid token!";

            return "Your password has been reset successfully.";
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user == null || user.IsDeleted)
                return "Email is incorrect!";

            if (changePasswordDto.CurrentPassword.Equals(changePasswordDto.NewPassword))
                return "New and old password cannot be the same!";

            var result = await _userManager.ChangePasswordAsync(user,
                changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
                return "An error occurred while changing the password!";

            return "Your password has been updated successfully.";
        }

        public async Task<string> ResetPasswordRequestAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || user.IsDeleted)
                return "The Email you Provided is not Correct!";
            //generating the token to verify the user's email
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            await _emailService.SendEmailAsync(email, "Password Reset Code.",
                $"Hello {user.UserName}, Use this new token to Reset your Password: {token}\n This code is Valid only for {expirationTime} Minutes.");
            return "A Password Reset Code has been sent to your Email!";
        }

        public async Task<ResetPasswordResponseModel> VerifyResetPasswordRequestAsync(ConfirmEmailDto confirmEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null || user.IsDeleted)
                return new ResetPasswordResponseModel { IsRequestVerified = false, Message = "User not found." };
            var result = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", confirmEmailDto.Token);
            if (!result)
                return new ResetPasswordResponseModel { IsRequestVerified = false, Message = "Token is not valid!" };

            return new ResetPasswordResponseModel
            {
                IsRequestVerified = true,
                Message = "Your Password reset request is verified."
            };
        }
    }
}
