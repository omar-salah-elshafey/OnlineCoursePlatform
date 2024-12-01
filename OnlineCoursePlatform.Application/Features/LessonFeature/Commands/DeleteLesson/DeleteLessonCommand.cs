using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.DeleteLesson
{
    public record DeleteLessonCommand(int Id) : IRequest<DeleteResponseModel>;
}
