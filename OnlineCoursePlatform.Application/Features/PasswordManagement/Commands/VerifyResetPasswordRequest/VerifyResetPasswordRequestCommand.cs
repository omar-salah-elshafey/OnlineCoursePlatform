using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public record VerifyResetPasswordRequestCommand(ConfirmEmailDto ConfirmEmailDto) : IRequest<ResetPasswordResponseModel>;
}
