using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.SearchUsersByName
{
    public record SearchUsersByNameQuery(string Keyword) : IRequest<List<UserResponseModel>>;
}
