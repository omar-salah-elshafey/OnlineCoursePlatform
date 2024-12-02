using MediatR;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.CreateEnrollment
{
    public record CreateEnrollmentCommand(EnrollmentDto EnrollmentDto, string CurrentUserId) : IRequest<EnrollmentResponseModel>;
}
