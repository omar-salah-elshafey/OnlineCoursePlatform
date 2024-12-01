using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.DeleteModule
{
    public record DeleteModuleCommand(int ModuleId, string CurrentUserId) : IRequest<DeleteResponseModel>;
}
