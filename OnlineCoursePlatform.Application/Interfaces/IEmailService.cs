using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task<AuthResponseModel> VerifyEmail(ConfirmEmailDto confirmEmailDto);
        Task<AuthResponseModel> ResendEmailConfirmationTokenAsync(string UserName);
    }
}
