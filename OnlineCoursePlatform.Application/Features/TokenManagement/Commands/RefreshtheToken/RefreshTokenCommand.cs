using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RefreshtheToken
{
    public record RefreshTokenCommand(string token) : IRequest<AuthResponseModel>;
}
