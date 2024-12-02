using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCoursesByInstructorId
{
    public record GetCoursesByInstructorIdQuery(string InstructorId) : IRequest<List<CourseResponseModel>>;
}
