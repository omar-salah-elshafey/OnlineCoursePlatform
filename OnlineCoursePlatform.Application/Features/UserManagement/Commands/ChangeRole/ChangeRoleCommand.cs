using MediatR;
using OnlineCoursePlatform.Application.DTOs;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(ChangeUserRoleDto changeRoleDto) : IRequest<string>;
}
