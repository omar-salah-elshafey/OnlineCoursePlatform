using MediatR;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<List<UserResponseModel>>;


}
