using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(LoginDto loginDto) : IRequest<AuthResponseModel>;
}
