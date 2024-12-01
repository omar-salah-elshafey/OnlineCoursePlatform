using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler(ICourseRepository _courseRepository, ILogger<GetCourseByIdQueryHandler> _logger,
        UserManager<User> _userManager) : IRequestHandler<GetCourseByIdQuery, CourseResponseModel>
    {
        public async Task<CourseResponseModel> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var courseId = request.CourseId;
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) 
            {
                _logger.LogError("Course Not Found.");
                return null;
            }
            var instructor = await _userManager.FindByIdAsync(course.InstructorId);
            _logger.LogInformation("Returnig the Course Details...");
            return new CourseResponseModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                IsPublished = course.IsPublished,
                Price = course.Price,
                InstructorName = $"{instructor.FirstName} {instructor.LastName}",
                InstructorId = instructor.Id,
                ThumbnailUrl = course.ThumbnailUrl,
                Modules = course.Modules.Select(m => new ModuleResponseModel { 
                    Id = m.Id, 
                    Title = m.Title, 
                    Description = m.Description,
                    Order = m.Order,
                    CourseName = m.Course.Title
                }).ToList()
            };
        }
    }
}
