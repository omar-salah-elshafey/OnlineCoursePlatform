using MediatR;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCourseById
{
    public record GetCourseByIdQuery(int CourseId) : IRequest<CourseResponseModel>;
}
