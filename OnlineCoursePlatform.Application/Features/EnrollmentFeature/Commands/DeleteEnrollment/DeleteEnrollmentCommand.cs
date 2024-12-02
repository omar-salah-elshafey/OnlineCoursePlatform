using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.DeleteEnrollment
{
    public record DeleteEnrollmentCommand(int EnrollmentId, string CurrentUserId) : IRequest<DeleteResponseModel>;
}
