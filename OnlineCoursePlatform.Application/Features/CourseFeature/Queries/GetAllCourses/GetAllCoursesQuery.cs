using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetAllCourses
{
    public record GetAllCoursesQuery() : IRequest<List<CourseResponseModel>>;
}
