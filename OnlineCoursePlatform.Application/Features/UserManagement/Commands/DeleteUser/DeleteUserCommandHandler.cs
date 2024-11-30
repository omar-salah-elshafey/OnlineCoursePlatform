using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Features.Authentication.Commands.Logout;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(UserManager<User> userManager, IMediator _mediator) : IRequestHandler<DeleteUserCommand, string>
    {

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var UserName = request.UserName;
            var CurrentUserName = request.CurrentUserName;
            var refreshToken = request.refreshToken;
            var user = await userManager.FindByNameAsync(UserName);
            var currentUser = await userManager.FindByNameAsync(CurrentUserName);
            var role = (await userManager.GetRolesAsync(currentUser)).First().ToUpper();
            if (user is null || user.IsDeleted)
                return $"User with UserName: {UserName} isn't found!";
            if (!CurrentUserName.Equals(UserName) && role != "ADMIN")
                return $"You Are Not Allowed to perform this action!";
            user.IsDeleted = true;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return $"An Error Occured while Deleting the user{UserName}";
            if (UserName == CurrentUserName)
                await _mediator.Send(new LogoutCommand(refreshToken, user.Id));
            return $"User with UserName: '{UserName}' has been Deleted successfully";
        }
    }
}
