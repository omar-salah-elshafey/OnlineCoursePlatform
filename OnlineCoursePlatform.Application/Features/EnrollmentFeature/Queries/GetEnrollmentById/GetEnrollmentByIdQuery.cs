using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentById
{
    public record GetEnrollmentByIdQuery(int EnrollmentId) : IRequest<EnrollmentResponseModel>;
}
