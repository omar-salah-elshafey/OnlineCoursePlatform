using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCoursesByInstructorId
{
    public record GetCoursesByInstructorIdQuery(string InstructorId) : IRequest<List<CourseResponseModel>>;
}
