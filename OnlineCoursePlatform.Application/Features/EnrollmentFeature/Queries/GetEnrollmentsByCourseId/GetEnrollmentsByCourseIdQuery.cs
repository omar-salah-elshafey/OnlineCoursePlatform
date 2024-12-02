using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByCourseId
{
    public record GetEnrollmentsByCourseIdQuery(int CourseId) : IRequest<List<EnrollmentResponseModel>>;
}
