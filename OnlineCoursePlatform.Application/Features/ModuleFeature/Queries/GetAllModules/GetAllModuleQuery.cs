using MediatR;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetAllModules
{
    public record GetAllModuleQuery : IRequest<List<ModuleResponseModel>>;
}
