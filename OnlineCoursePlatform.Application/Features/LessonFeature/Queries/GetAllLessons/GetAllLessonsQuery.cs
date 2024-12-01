using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetAllLessons
{
    public record GetAllLessonsQuery : IRequest<List<LessonResponseModel>>;
}
