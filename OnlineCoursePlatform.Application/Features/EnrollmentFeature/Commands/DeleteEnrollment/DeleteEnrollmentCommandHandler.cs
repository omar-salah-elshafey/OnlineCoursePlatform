using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommandHandler(IEnrollmentRepository _enrollmentRepository, UserManager<User> _userManager,
        ILogger<DeleteEnrollmentCommandHandler> logger) 
        : IRequestHandler<DeleteEnrollmentCommand, DeleteResponseModel>
    {
        public async Task<DeleteResponseModel> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = request.CurrentUserId;
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId);
            if (enrollment == null)
            {
                logger.LogError("Enrollment not Found.");
                return new DeleteResponseModel { IsDeleted = false, Message = "Enrollment not Found." };
            }
                
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var isAdmin = await _userManager.IsInRoleAsync(currentUser!, "Admin");
            var isInstructor = await _userManager.IsInRoleAsync(currentUser!, "Instructor");
            if (!currentUserId.Equals(enrollment.StudentId) && !isAdmin && !isInstructor)
            {
                logger.LogError("Unauthorized action.");
                return new DeleteResponseModel { IsDeleted = false, Message = "You are not authorized to do this action." };
            }
            
            await _enrollmentRepository.DeleteEnrollmentAsync(enrollment);
            logger.LogInformation("Enrollment Deletd.");
            return new DeleteResponseModel { IsDeleted = true, Message = "Enrollment has been Deleted Successfullly." };
        }
    }
}
