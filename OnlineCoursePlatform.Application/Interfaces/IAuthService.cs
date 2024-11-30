using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> RegisterUserAsync(RegistrationDto registrationDto, string role);
        Task<AuthResponseModel> LoginAsync(LoginDto loginDto);
        Task<bool> LogoutAsync(string refreshToken, string userId);
    }
}
