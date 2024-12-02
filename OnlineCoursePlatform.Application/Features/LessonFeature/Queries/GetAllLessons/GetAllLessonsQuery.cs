using MediatR;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetAllLessons
{
    public record GetAllLessonsQuery : IRequest<List<LessonResponseModel>>;
}
