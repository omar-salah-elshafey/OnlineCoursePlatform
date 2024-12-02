using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsersByRole
{
    public record GetUsersByRoleQuery(string Role) : IRequest<List<UserResponseModel>>;
}
