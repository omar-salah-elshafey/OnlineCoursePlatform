using MediatR;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.CreateLesson
{
    public record CreateLessonCommand(LessonDto LessonDto) : IRequest<LessonResponseModel>;
}
