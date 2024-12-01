using MediatR;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public record DeleteCourseCommand(int Id) : IRequest<bool>;
}