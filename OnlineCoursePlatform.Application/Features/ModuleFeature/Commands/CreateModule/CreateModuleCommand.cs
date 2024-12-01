using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.CreateModule
{
    public record CreateModuleCommand(ModuleDto ModuleDto) : IRequest<ModuleResponseModel>;
}
