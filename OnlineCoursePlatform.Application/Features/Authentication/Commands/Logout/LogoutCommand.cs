using MediatR;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.Logout
{
    public record LogoutCommand(string refreshToken, string userId) : IRequest<bool>;
}
