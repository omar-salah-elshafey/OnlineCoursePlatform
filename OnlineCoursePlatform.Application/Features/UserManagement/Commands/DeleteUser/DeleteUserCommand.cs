using MediatR;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.DeleteUser
{
    public record DeleteUserCommand(string UserName, string CurrentUserName, string refreshToken) : IRequest<string>;
}
