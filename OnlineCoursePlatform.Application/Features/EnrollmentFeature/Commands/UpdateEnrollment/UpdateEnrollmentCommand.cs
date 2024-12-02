using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.UpdateEnrollment
{
    public record UpdateEnrollmentCommand(int EnrollmentId, UpdateEnrollmentDto UpdateDto, string CurrentUserId) 
        : IRequest<EnrollmentResponseModel>;
}
