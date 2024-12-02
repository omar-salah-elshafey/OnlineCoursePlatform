using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetAllCourses
{
    public class GetAllCoursesQueryHandler(ICourseRepository _courseRepository, ILogger<GetAllCoursesQueryHandler> _logger,
        UserManager<User> _userManager) : IRequestHandler<GetAllCoursesQuery, List<CourseResponseModel>>
    {
        public async Task<List<CourseResponseModel>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            var courseResponseModels = new List<CourseResponseModel>();
            foreach (var course in courses)
            {
                var instructor = await _userManager.FindByIdAsync(course.InstructorId);
                courseResponseModels.Add(new CourseResponseModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    IsPublished = course.IsPublished,
                    Price = course.Price,
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
                });
            }

            return courseResponseModels;
        }
    }
}
