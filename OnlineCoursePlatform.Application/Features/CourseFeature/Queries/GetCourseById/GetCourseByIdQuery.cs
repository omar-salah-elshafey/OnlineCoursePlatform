using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCourseById
{
    public record GetCourseByIdQuery(int CourseId) : IRequest<CourseResponseModel>;
}
