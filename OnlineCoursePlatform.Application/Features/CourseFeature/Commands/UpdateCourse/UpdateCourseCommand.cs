using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public record UpdateCourseCommand(int Id, UpdateCourseDto updateCourseDto, string CurrentUserId) : IRequest<CourseResponseModel>;
}
