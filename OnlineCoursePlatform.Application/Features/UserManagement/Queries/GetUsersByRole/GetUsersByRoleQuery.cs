using MediatR;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsersByRole
{
    public record GetUsersByRoleQuery(string Role) : IRequest<List<UserResponseModel>>;
}
