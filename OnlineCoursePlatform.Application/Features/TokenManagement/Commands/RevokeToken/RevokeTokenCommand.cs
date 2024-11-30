using MediatR;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RevokeToken
{
    public record RevokeTokenCommand(string token): IRequest<bool>;
}
