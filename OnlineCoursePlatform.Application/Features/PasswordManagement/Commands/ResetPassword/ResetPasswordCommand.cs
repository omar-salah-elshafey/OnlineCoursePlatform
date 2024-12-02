using MediatR;
using OnlineCoursePlatform.Application.Features.PasswordManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordDto resetPasswordDto): IRequest<string>;
}
