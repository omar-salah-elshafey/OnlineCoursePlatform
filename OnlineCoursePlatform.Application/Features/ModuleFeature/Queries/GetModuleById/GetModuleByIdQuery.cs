using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModuleById
{
    public record GetModuleByIdQuery(int ModuleId) : IRequest<ModuleResponseModel>;
}
