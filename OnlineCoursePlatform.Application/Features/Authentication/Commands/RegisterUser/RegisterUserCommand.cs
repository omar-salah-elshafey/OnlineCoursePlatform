using MediatR;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(RegistrationDto registrationDto, string role): IRequest<AuthResponseModel>;
}
