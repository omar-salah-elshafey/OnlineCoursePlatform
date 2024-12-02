using MediatR;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Commands.UpdateUser
{
    public record UpdateUserCommand(UpdateUserDto UpdateUserDto) : IRequest<UpdateUserResponseModel>;
}
