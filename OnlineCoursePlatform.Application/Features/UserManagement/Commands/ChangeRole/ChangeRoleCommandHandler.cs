using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<ChangeRoleCommand, string>
    {
        public async Task<string> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var changeRoleDto = request.changeRoleDto;
            var user = await userManager.FindByNameAsync(changeRoleDto.UserName);
            if (user == null || user.IsDeleted)
                return ("Invalid UserName!");
            if (!await roleManager.RoleExistsAsync(changeRoleDto.Role))
                return ("Invalid Role!");
            if (await userManager.IsInRoleAsync(user, changeRoleDto.Role))
                return ("User Is already assigned to this role!");
            var currentrole = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            await userManager.RemoveFromRoleAsync(user, currentrole);
            var result = await userManager.AddToRoleAsync(user, changeRoleDto.Role);
            return $"User {changeRoleDto.UserName} has been assignd to Role {changeRoleDto.Role} Successfully :)";
        }
    }
}
