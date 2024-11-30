using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers
{
    public class GetUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.Where(u => !u.IsDeleted).ToListAsync(cancellationToken);
            
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                    var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
                    userDtos.Add(new UserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                    });
            }
            return userDtos;
        }
    }
}
