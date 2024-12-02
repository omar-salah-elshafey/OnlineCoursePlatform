using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers
{
    public class GetUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUsersQuery, List<UserResponseModel>>
    {
        public async Task<List<UserResponseModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.Where(u => !u.IsDeleted).ToListAsync(cancellationToken);
            
            var userDtos = new List<UserResponseModel>();
            foreach (var user in users)
            {
                    var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
                    userDtos.Add(new UserResponseModel
                    {
                        Id = user.Id,
                        Name = $"{user.FirstName} {user.LastName}",
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                    });
            }
            return userDtos;
        }
    }
}
