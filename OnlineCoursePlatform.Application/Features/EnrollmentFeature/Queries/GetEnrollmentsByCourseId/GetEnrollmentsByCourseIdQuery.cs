using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByCourseId
{
    public record GetEnrollmentsByCourseIdQuery(int CourseId) : IRequest<List<EnrollmentResponseModel>>;
}
