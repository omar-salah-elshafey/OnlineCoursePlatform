using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IUserManagementService
    {
        Task<List<UserDto>> GetUSersAsync();
        Task<List<UserResponseModel>> GetUsersByRoleAsync(string role);
        Task<string> ChangeRoleAsync(ChangeUserRoleDto changeRoleDto);
        Task<string> DeleteUserAsync(string UserName, string CurrentUserName, string refreshToken);
        Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<List<UserResponseModel>> SearchUsersByNameAsync(string keyword);
    }
}
