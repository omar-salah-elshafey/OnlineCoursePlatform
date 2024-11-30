using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(RegistrationDto registrationDto, string role): IRequest<AuthResponseModel>;
}
