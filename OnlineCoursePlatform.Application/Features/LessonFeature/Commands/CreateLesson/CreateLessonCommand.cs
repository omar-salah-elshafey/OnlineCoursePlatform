using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.CreateLesson
{
    public record CreateLessonCommand(LessonDto LessonDto) : IRequest<LessonResponseModel>;
}
