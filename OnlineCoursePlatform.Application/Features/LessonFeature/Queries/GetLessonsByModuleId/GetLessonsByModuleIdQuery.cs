using MediatR;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonsByModuleId
{
    public record GetLessonsByModuleIdQuery(int ModuleId) : IRequest<List<LessonResponseModel>>;
}
