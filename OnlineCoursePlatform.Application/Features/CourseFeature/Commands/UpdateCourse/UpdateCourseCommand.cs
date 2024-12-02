using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public record UpdateCourseCommand(int Id, UpdateCourseDto updateCourseDto, string CurrentUserId) : IRequest<CourseResponseModel>;
}
