using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IAuthService _authService;
        public UserManagementService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<List<UserDto>> GetUSersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                if (!user.IsDeleted)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    userDtos.Add(new UserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                    });
                }
            }
            return userDtos;
        }

        public async Task<string> ChangeRoleAsync(ChangeUserRoleDto changeRoleDto)
        {
            var user = await _userManager.FindByNameAsync(changeRoleDto.UserName);
            if (user == null || user.IsDeleted)
                return ("Invalid UserName!");
            if (!await _roleManager.RoleExistsAsync(changeRoleDto.Role))
                return ("Invalid Role!");
            if (await _userManager.IsInRoleAsync(user, changeRoleDto.Role))
                return ("User Is already assigned to this role!");
            var result = await _userManager.AddToRoleAsync(user, changeRoleDto.Role);
            return $"User {changeRoleDto.UserName} has been assignd to Role {changeRoleDto.Role} Successfully :)";
        }

        public async Task<string> DeleteUserAsync(string UserName, string CurrentUserName, string refreshToken)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var currentUser = await _userManager.FindByNameAsync(CurrentUserName);
            var role = (await _userManager.GetRolesAsync(currentUser)).First().ToUpper();
            if (user is null || user.IsDeleted)
                return $"User with UserName: {UserName} isn't found!";
            if (!CurrentUserName.Equals(UserName) && role != "ADMIN")
                return $"You Are Not Allowed to perform this action!";
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return $"An Error Occured while Deleting the user{UserName}";
            if (UserName == CurrentUserName)
                await _authService.LogoutAsync(refreshToken, user.Id);
            return $"User with UserName: '{UserName}' has been Deleted successfully";
        }

        public async Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByNameAsync(updateUserDto.UserName);
            if (user is null || user.IsDeleted == true)
                return new UpdateUserResponseModel { Message = $"User with UserName: {updateUserDto.UserName} isn't found!" };
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                return new UpdateUserResponseModel { Message = $"Failed to update user: {errors}" };
            }
            return new UpdateUserResponseModel
            {
                UserName = updateUserDto.UserName,
                FirstName = updateUserDto.FirstName,
                LastName = updateUserDto.LastName,
                Message = "User has been updated Successfully."
            };
        }
    }
}
