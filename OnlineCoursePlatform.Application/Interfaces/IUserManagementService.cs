using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IUserManagementService
    {
        Task<List<UserDto>> GetUSersAsync();
        Task<string> ChangeRoleAsync(ChangeUserRoleDto changeRoleDto);
        Task<string> DeleteUserAsync(string UserName, string CurrentUserName, string refreshToken);
        Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
