using MediatR;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.UpdateEnrollment
{
    public record UpdateEnrollmentCommand(int EnrollmentId, UpdateEnrollmentDto UpdateDto, string CurrentUserId) 
        : IRequest<EnrollmentResponseModel>;
}
