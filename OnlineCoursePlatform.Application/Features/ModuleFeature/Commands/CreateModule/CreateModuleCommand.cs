using MediatR;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.CreateModule
{
    public record CreateModuleCommand(ModuleDto ModuleDto) : IRequest<ModuleResponseModel>;
}
