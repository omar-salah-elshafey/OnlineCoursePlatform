using MediatR;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RefreshtheToken
{
    public record RefreshTokenCommand(string token) : IRequest<AuthResponseModel>;
}
