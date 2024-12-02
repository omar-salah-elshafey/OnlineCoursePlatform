using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface IUserManagementService
    {
        Task<List<UserResponseModel>> GetUSersAsync();
        Task<List<UserResponseModel>> GetUsersByRoleAsync(string role);
        Task<string> ChangeRoleAsync(ChangeUserRoleDto changeRoleDto);
        Task<string> DeleteUserAsync(string UserName, string CurrentUserName, string refreshToken);
        Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<List<UserResponseModel>> SearchUsersByNameAsync(string keyword);
    }
}
