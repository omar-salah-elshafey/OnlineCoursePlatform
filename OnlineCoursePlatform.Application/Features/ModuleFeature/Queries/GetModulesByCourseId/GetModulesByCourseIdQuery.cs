using MediatR;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModulesByCourseId
{
    public record GetModulesByCourseIdQuery(int CourseId) : IRequest<List<ModuleResponseModel>>;
}
