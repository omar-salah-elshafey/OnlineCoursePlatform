using MediatR;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.UpdateModule
{
    public record UpdateModuleCommand(int ModuleId, UpdateModuleDto UpdateModuleDto, string CurrentUserId) 
        : IRequest<ModuleResponseModel>;
}
