using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ResetPasswordRequest
{
    public class ResetPasswordRequestCommandHandler(UserManager<User> _userManager, IEmailService _emailService,
        IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions
        , ILogger<ResetPasswordRequestCommandHandler> logger)
        : IRequestHandler<ResetPasswordRequestCommand, string>
    {
        public async Task<string> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var email = request.email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || user.IsDeleted)
            {
                logger.LogWarning("The Email you Provided is not Correct!");
                return "The Email you Provided is not Correct!";
            }
                
            //generating the token to verify the user's email
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            await _emailService.SendEmailAsync(email, "Password Reset Code.",
                $"Hello {user.UserName}, Use this new token to Reset your Password: {token}\n This code is Valid only for {expirationTime} Minutes.");
            logger.LogInformation("A Password Reset Code has been sent to your Email!");
            return "A Password Reset Code has been sent to your Email!";
        }
    }
}
