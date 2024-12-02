using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions;

        public EmailService(IConfiguration config, UserManager<User> userManager,
            IOptions<DataProtectionTokenProviderOptions> tokenProviderOptions)
        {
            _config = config;
            _userManager = userManager;
            _tokenProviderOptions = tokenProviderOptions;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task<AuthResponseModel> VerifyEmail(ConfirmEmailDto confirmEmailDto)
        {

            if (string.IsNullOrEmpty(confirmEmailDto.Email) || string.IsNullOrEmpty(confirmEmailDto.Token))
                return new AuthResponseModel { IsConfirmed = false, Message = "User ID and token are required." };

            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null || user.IsDeleted)
                return new AuthResponseModel { IsConfirmed = false, Message = "User not found." };
            if (user.EmailConfirmed)
                return new AuthResponseModel { Message = "Your Email is already confirmed" };
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded)
                return new AuthResponseModel { IsConfirmed = false, Message = "Token is not valid!" };

            return new AuthResponseModel { IsConfirmed = true, Message = "Your Email has been confirmed successfully :) " };
        }

        public async Task<AuthResponseModel> ResendEmailConfirmationTokenAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null || user.IsDeleted)
                return new AuthResponseModel { IsConfirmed = false, Message = "User not found." };

            if (await _userManager.IsEmailConfirmedAsync(user))
                return new AuthResponseModel { IsConfirmed = true, Message = "Email is already confirmed." };

            // Generate new token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;
            // Send the new token via email
            await SendEmailAsync(user.Email, "Email Verification Code",
                $"Hello {user.UserName}, Use this new token to verify your Email: {token}\n This code is Valid only for {expirationTime} Minutes.");

            return new AuthResponseModel { IsConfirmed = false, Message = "A new verification email has been sent." };
        }
    }
}
