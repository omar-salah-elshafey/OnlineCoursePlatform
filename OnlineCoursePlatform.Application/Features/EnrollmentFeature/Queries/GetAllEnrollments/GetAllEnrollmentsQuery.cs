using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetAllEnrollments
{
    public record GetAllEnrollmentsQuery : IRequest<List<EnrollmentResponseModel>>
    {
    }
}
