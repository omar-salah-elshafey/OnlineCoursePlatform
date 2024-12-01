using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetAllModules
{
    public record GetAllModuleQuery : IRequest<List<ModuleResponseModel>>;
}
