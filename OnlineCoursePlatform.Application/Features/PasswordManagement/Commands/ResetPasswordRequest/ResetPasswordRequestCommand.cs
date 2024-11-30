using MediatR;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.ResetPasswordRequest
{
    public record ResetPasswordRequestCommand(string email) : IRequest<string>;
}
