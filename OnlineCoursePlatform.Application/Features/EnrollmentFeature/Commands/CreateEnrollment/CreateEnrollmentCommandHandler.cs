using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, UserManager<User> _userManager,
        ILogger<CreateEnrollmentCommandHandler> logger)
        : IRequestHandler<CreateEnrollmentCommand, EnrollmentResponseModel>
    {
        public async Task<EnrollmentResponseModel> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attemping to create a new enrollment...");
            var dto = request.EnrollmentDto;
            var studentID =dto.StudentId;
            var currentUserId = request.CurrentUserId;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var isAdmin = await _userManager.IsInRoleAsync(currentUser!, "Admin");
            var isInstructor = await _userManager.IsInRoleAsync(currentUser!, "Instructor");
            if (!studentID.Equals(currentUserId) && !isAdmin && !isInstructor)
            {
                logger.LogError("Unauthorized action.");
                return new EnrollmentResponseModel { Message = "You are not authorized to do this action." };
            }

            var existingEnrollment = await enrollmentRepository.GetEnrollmentByStudentAndCourseAsync(studentID, dto.CourseId);
            if (existingEnrollment != null)
            {
                logger.LogError("Enrollment already exists for this student and course.");
                return new EnrollmentResponseModel { Message = "An enrollment with the same student and course already exists." };
            }

            logger.LogInformation("Creating a new enrollment...");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = DateTime.UtcNow,
                Progress = 0,
                IsCompleted = false
            };

            await enrollmentRepository.CreateEnrollmentAsync(enrollment);
            await enrollmentRepository.SaveChangesAsync();
            logger.LogInformation("A new enrollment Created.");

            var savedEnrollment = await enrollmentRepository.GetEnrollmentByIdAsync(enrollment.Id);
            return new EnrollmentResponseModel
            {
                Id = savedEnrollment.Id,
                StudentId = savedEnrollment.StudentId,
                StudentName = $"{savedEnrollment.Student.FirstName} {savedEnrollment.Student.LastName}",
                CourseId = savedEnrollment.Course.Id,
                CourseTitle = savedEnrollment.Course.Title,
                EnrollmentDate = savedEnrollment.EnrollmentDate,
                Progress = savedEnrollment.Progress,
                IsCompleted = savedEnrollment.IsCompleted
            };
        }
    }
}
