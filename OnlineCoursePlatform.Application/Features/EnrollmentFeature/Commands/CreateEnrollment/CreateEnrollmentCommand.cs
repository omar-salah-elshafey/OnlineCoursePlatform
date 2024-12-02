using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.CreateEnrollment
{
    public record CreateEnrollmentCommand(EnrollmentDto EnrollmentDto, string CurrentUserId) : IRequest<EnrollmentResponseModel>;
}
