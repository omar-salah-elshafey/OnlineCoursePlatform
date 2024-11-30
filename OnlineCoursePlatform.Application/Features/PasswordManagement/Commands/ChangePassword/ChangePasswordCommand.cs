using MediatR;
using OnlineCoursePlatform.Application.DTOs;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordDto changePasswordDto): IRequest<string>;
}
