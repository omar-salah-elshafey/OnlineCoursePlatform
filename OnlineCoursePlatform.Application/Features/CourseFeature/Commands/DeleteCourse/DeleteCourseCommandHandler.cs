using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler(ICourseRepository _courseRepository, ILogger<DeleteCourseCommandHandler> _logger,
        UserManager<User> _userManager)
        : IRequestHandler<DeleteCourseCommand, DeleteResponseModel>
    {
        public async Task<DeleteResponseModel> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = request.CurrentUserId;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            var course = await _courseRepository.GetCourseByIdAsync(request.Id);
            if (course == null)
            {
                _logger.LogError("Course not found.");
                return new DeleteResponseModel
                {
                    IsDeleted = false,
                    Message = "Course not found."
                };
            }

            var instructorID = course.InstructorId;
            var isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            if (!instructorID.Equals(currentUserId) && !isAdmin)
            {
                _logger.LogError("Unauthorized action.");
                return new DeleteResponseModel
                {
                    IsDeleted = false,
                    Message = "You are not authorized to delete this course."
                };
            }

            await _courseRepository.DeleteCourseAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Course deleted successfully.");
            return new DeleteResponseModel
            {
                IsDeleted = true,
                Message = "Course has been deleted successfully."
            };
        }
    }
}
