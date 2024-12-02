using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.PasswordManagement.Dtos;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IPasswordManagementService
    {
        Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<string> ResetPasswordRequestAsync(string email);
        Task<ResetPasswordResponseModel> VerifyResetPasswordRequestAsync(ConfirmEmailDto confirmEmailDto);
    }
}
