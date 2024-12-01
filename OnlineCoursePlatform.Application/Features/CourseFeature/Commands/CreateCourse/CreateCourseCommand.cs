using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.CreateCourse
{
    public record CreateCourseCommand(CourseDto CourseDto, string userId) : IRequest<CourseResponseModel>;
}
