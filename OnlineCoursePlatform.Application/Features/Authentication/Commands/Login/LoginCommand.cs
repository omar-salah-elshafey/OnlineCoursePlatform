using MediatR;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(LoginDto loginDto) : IRequest<AuthResponseModel>;
}
