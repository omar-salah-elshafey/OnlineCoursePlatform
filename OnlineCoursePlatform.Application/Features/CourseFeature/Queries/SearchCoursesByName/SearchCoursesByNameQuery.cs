using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.SearchCoursesByName
{
    public record SearchCoursesByNameQuery(string Keyword) : IRequest<List<CourseResponseModel>>;
}
