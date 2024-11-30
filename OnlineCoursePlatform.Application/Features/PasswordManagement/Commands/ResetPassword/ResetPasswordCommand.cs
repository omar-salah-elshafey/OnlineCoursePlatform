using MediatR;
using OnlineCoursePlatform.Application.DTOs;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordDto resetPasswordDto): IRequest<string>;
}
