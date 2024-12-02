using MediatR;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModuleById
{
    public record GetModuleByIdQuery(int ModuleId) : IRequest<ModuleResponseModel>;
}
