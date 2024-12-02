using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(UserManager<User> userManager, ICookieService cookieService, 
        ILogger<UpdateUserCommandHandler> logger) 
        : IRequestHandler<UpdateUserCommand, UpdateUserResponseModel>
    {
        public async Task<UpdateUserResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUserDto = request.UpdateUserDto;
            var user = await userManager.FindByNameAsync(updateUserDto.UserName);
            if (user is null || user.IsDeleted == true)
            {
                logger.LogWarning($"User with UserName: {updateUserDto.UserName} isn't found!");
                return new UpdateUserResponseModel { Message = $"User with UserName: {updateUserDto.UserName} isn't found!" };
            }
                
            var currentUserName = cookieService.GetFromCookies("userName");
            var currentUser = await userManager.FindByNameAsync(currentUserName);
            var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");
            if (!currentUserName.Equals(updateUserDto.UserName) && !isAdmin)
            {
                logger.LogWarning("You Can't Do this Action!");
                return new UpdateUserResponseModel { Message = "You Can't Do this Action!" };
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                logger.LogError(errors);
                return new UpdateUserResponseModel { Message = $"Failed to update user: {errors}" };
            }
            logger.LogInformation("User has been updated Successfully.");
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
