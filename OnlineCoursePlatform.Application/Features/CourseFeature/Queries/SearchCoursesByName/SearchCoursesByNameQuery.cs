using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.SearchCoursesByName
{
    public record SearchCoursesByNameQuery(string Keyword) : IRequest<List<CourseResponseModel>>;
}
