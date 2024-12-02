using MediatR;
using OnlineCoursePlatform.Application.Features.PasswordManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordDto changePasswordDto): IRequest<string>;
}
