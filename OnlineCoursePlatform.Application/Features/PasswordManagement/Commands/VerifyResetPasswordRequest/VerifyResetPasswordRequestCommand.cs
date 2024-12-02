using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.PasswordManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public record VerifyResetPasswordRequestCommand(ConfirmEmailDto ConfirmEmailDto) : IRequest<ResetPasswordResponseModel>;
}
