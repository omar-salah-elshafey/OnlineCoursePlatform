using MediatR;
using OnlineCoursePlatform.Application.DTOs;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<List<UserDto>>;


}
