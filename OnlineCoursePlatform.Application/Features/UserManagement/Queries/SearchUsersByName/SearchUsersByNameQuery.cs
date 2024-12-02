using MediatR;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.SearchUsersByName
{
    public record SearchUsersByNameQuery(string Keyword) : IRequest<List<UserResponseModel>>;
}
