using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public record DeleteCourseCommand(int Id, string CurrentUserId) : IRequest<DeleteResponseModel>;
}