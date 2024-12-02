using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonsByModuleId
{
    public record GetLessonsByModuleIdQuery(int ModuleId) : IRequest<List<LessonResponseModel>>;
}
