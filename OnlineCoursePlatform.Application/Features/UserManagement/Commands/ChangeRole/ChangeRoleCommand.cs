using MediatR;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(ChangeUserRoleDto changeRoleDto) : IRequest<string>;
}
