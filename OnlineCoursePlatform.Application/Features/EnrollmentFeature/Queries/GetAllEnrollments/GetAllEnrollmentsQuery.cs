using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetAllEnrollments
{
    public record GetAllEnrollmentsQuery : IRequest<List<EnrollmentResponseModel>>
    {
    }
}
