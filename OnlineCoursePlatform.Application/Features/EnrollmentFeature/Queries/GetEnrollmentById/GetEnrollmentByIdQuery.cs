using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentById
{
    public record GetEnrollmentByIdQuery(int EnrollmentId) : IRequest<EnrollmentResponseModel>;
}
