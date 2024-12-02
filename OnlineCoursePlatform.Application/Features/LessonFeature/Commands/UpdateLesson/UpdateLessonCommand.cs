using MediatR;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.UpdateLesson
{
    public record UpdateLessonCommand(int Id, LessonDto LessonDto) : IRequest<LessonResponseModel>;
}
