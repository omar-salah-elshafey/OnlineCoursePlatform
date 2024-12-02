using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.UpdateEnrollment
{
    public class UpdateEnrollmentCommandHandler(IEnrollmentRepository _enrollmentRepository, UserManager<User> _userManager,
        ILogger<UpdateEnrollmentCommandHandler> logger)
        : IRequestHandler<UpdateEnrollmentCommand, EnrollmentResponseModel>
    {
        public async Task<EnrollmentResponseModel> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId);
            if (enrollment == null )
            {
                logger.LogError("Enrollment not Found.");
                return new EnrollmentResponseModel { Message = "Enrollment not Found." };
            }
                
            var currentUserId = request.CurrentUserId;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var isAdmin = await _userManager.IsInRoleAsync(currentUser!, "Admin");
            var isInstructor = await _userManager.IsInRoleAsync(currentUser!, "Instructor");
            if (!currentUserId.Equals(enrollment.StudentId) && !isAdmin && !isInstructor)
            {
                logger.LogError("Unauthorized action.");
                return new EnrollmentResponseModel { Message = "You are not authorized to do this action." };
            }

            var dto = request.UpdateDto;
            if(dto.Progress > 100)
            {
                logger.LogError("Progress Can't be > 100");
                return new EnrollmentResponseModel { Message = "Progress Can't be > 100" };
            }
            enrollment.Progress = dto.Progress;
            enrollment.IsCompleted = dto.IsCompleted;
            if (dto.CompletionDate != null)
                enrollment.CompletionDate = dto.CompletionDate;
            else dto.CompletionDate = null;

            await _enrollmentRepository.SaveChangesAsync();
            logger.LogInformation("Enrollment Updated.");

            return new EnrollmentResponseModel
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = $"{enrollment.Student.FirstName} {enrollment.Student.LastName}",
                CourseId = enrollment.Course.Id,
                CourseTitle = enrollment.Course.Title,
                EnrollmentDate = enrollment.EnrollmentDate,
                Progress = enrollment.Progress,
                IsCompleted = enrollment.IsCompleted,
                CompletionDate = enrollment.CompletionDate
            };
        }
    }
}
