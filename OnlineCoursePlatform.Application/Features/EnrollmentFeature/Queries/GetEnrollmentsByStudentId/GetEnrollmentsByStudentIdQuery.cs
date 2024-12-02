using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByStudentId
{
    public record GetEnrollmentsByStudentIdQuery(string StudentId) : IRequest<List<EnrollmentResponseModel>>;
}
