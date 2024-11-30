using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.UpdateUser
{
    public record UpdateUserCommand(UpdateUserDto UpdateUserDto) : IRequest<UpdateUserResponseModel>;
}
