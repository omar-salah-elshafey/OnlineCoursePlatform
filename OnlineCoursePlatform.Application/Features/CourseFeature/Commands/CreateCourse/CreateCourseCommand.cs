using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.CreateCourse
{
    public record CreateCourseCommand(CourseDto CourseDto, string userId) : IRequest<CourseResponseModel>;
}
