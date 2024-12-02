using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModulesByCourseId
{
    public record GetModulesByCourseIdQuery(int CourseId) : IRequest<List<ModuleResponseModel>>;
}
