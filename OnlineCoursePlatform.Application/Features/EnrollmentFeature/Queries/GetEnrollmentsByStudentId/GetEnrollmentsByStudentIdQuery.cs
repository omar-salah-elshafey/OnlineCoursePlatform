using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByStudentId
{
    public record GetEnrollmentsByStudentIdQuery(string StudentId) : IRequest<List<EnrollmentResponseModel>>;
}
