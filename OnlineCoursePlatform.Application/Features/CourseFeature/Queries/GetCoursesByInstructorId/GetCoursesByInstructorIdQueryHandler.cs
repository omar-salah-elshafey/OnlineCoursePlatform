using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCoursesByInstructorId
{
    public class GetCoursesByInstructorIdQueryHandler(ICourseRepository _courseRepository, 
        ILogger<GetCoursesByInstructorIdQueryHandler> _logger) 
        : IRequestHandler<GetCoursesByInstructorIdQuery, List<CourseResponseModel>>
    {
        public async Task<List<CourseResponseModel>> Handle(GetCoursesByInstructorIdQuery request, CancellationToken cancellationToken)
        {
            var instructorId = request.InstructorId;
            _logger.LogInformation("Fetching courses for instructor with ID {InstructorId}", instructorId);

            var courses = await _courseRepository.GetCoursesByInstructorIdAsync(instructorId);
            if (courses.Count == 0)
            {
                _logger.LogWarning("No courses found for instructor with ID {InstructorId}", instructorId);
                return new List<CourseResponseModel>();
            }

            _logger.LogInformation("Returning courses for instructor with ID {InstructorId}", instructorId);

            return courses.Select(c => new CourseResponseModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsPublished = c.IsPublished,
                Price = c.Price,
                ThumbnailUrl = c.ThumbnailUrl,
                InstructorId = c.InstructorId,
                InstructorName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
                Modules = c.Modules.Select(m => new ModuleResponseModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Order = m.Order
                }).ToList()
            }).ToList();
        }
    }
}
