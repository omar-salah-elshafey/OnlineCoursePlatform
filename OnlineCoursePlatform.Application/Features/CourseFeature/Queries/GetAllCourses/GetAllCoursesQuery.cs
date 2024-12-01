using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetAllCourses
{
    public record GetAllCoursesQuery() : IRequest<List<CourseResponseModel>>;
}
