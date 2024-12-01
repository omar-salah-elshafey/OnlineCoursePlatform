using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.CreateCourse
{
    public class CreateCourseCommandHandler(ICourseRepository _courseRepository, ILogger<CreateCourseCommandHandler> _logger,
        UserManager<User> _userManager) : IRequestHandler<CreateCourseCommand, CourseResponseModel>
    {
        public async Task<CourseResponseModel> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseDto = request.CourseDto;
            var instructorId = request.userId;
            var instructor = await _userManager.FindByIdAsync(instructorId);
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
                return new CourseResponseModel { Message = ex.Message};
            }
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Price = courseDto.Price,
                InstructorId = instructorId
            };



            await _courseRepository.AddCourseAsync(course);
            await _courseRepository.SaveChangesAsync();

            return new CourseResponseModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                IsPublished = course.IsPublished,
                Price = course.Price,
                InstructorName = $"{instructor.FirstName} {instructor.LastName}",
                InstructorId = instructorId,
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
