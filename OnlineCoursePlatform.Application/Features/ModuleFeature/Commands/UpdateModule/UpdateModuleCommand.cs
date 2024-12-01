using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.UpdateModule
{
    public record UpdateModuleCommand(int ModuleId, UpdateModuleDto UpdateModuleDto, string CurrentUserId) 
        : IRequest<ModuleResponseModel>;
}
