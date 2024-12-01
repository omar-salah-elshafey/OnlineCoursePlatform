using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler(ICourseRepository _courseRepository, UserManager<User> _userManager,
        ILogger<UpdateCourseCommandHandler> _logger) : IRequestHandler<UpdateCourseCommand, CourseResponseModel>
    {
        public async Task<CourseResponseModel> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseId = request.Id;
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            try
            {
                if (course == null)
                {
                    _logger.LogError("Course Not Found.");
                    throw new ArgumentNullException("Course not found.");
                }
            }
            catch (ArgumentNullException ex)
            {
                return new CourseResponseModel { Message = ex.Message };
            }


            var courseDto = request.updateCourseDto;
            var instructor = await _userManager.FindByIdAsync(courseDto.InstructorId);
            try
            {
                if (instructor == null)
                {
                    _logger.LogError("InstructorId You Entered is not valid.");
                    throw new ArgumentNullException("InstructorId You Entered is not valid.");
                }
            }
            catch (ArgumentNullException ex)
            {
                return new CourseResponseModel { Message = ex.Message };
            }

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Price = courseDto.Price;
            if (courseDto.InstructorId != null)
            {
                course.InstructorId = courseDto.InstructorId;
            }
            course.IsPublished = courseDto.IsPublished;
            course.ThumbnailUrl = courseDto.ThumbnailUrl;

            await _courseRepository.SaveChangesAsync();
            _logger.LogInformation("Course Detailse has been Updated Successfully.");
            return new CourseResponseModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                IsPublished = course.IsPublished,
                InstructorName = $"{instructor.FirstName} {instructor.LastName}",
                InstructorId = instructor.Id,
                ThumbnailUrl = course.ThumbnailUrl,
                Modules = course.Modules.Select(m => new ModuleResponseModel
                {
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
