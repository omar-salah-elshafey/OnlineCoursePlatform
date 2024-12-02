using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;
using System.Data;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(UserManager<User> _userManager, ILogger<RegisterUserCommandHandler> _logger,
        IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions, IEmailService _emailService)
        : IRequestHandler<RegisterUserCommand, AuthResponseModel>
    {
        public async Task<AuthResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registrationDto = request.registrationDto;
            var role = request.role;
            //check if user exists
            if (await _userManager.FindByEmailAsync(registrationDto.Email) is not null)
            {
                _logger.LogWarning($"This Email: '{registrationDto.Email}' is already used!");
                return new AuthResponseModel { Message = "This Email is already used!" };
            }
                
            if (await _userManager.FindByNameAsync(registrationDto.UserName) is not null)
            {
                _logger.LogWarning($"This UserName: '{registrationDto.UserName}' is already used!");
                return new AuthResponseModel { Message = "This UserName is already used!" };
            }

            // Create the new user
            var user = new User
            {
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                UserName = registrationDto.UserName,
                Email = registrationDto.Email
            };
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                _logger.LogError(errors);
                return new AuthResponseModel { Message = errors };
            }
            // Assign the user to the specified role
            await _userManager.AddToRoleAsync(user, role);

            //generating the token to verify the user's email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            await _emailService.SendEmailAsync(user.Email, "Email Verification Code.",
                $"Hello {user.UserName}, Use this new token to verify your Email: {token}{Environment.NewLine}This code is Valid only for {expirationTime} Minutes.");
            _logger.LogInformation("A verification code has been sent to your Email." +
                $"{Environment.NewLine}Verify Your Email to be able to login :) ");
            return new AuthResponseModel
            {
                Email = user.Email,
                Username = user.UserName,
                Message = "A verification code has been sent to your Email." +
                $"{Environment.NewLine}Verify Your Email to be able to login :) "
            };
        }
    }
}
