using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.UpdateLesson
{
    public record UpdateLessonCommand(int Id, LessonDto LessonDto) : IRequest<LessonResponseModel>;
}
