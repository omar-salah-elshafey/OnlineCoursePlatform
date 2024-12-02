using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModulesByCourseId
{
    public class GetModulesByCourseIdQueryHandler(IModuleRepository moduleRepository, ILogger<GetModulesByCourseIdQueryHandler> logger) 
        : IRequestHandler<GetModulesByCourseIdQuery, List<ModuleResponseModel>>
    {
        public async Task<List<ModuleResponseModel>> Handle(GetModulesByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var courseId = request.CourseId;
            logger.LogInformation("Fetching modules for course with ID {courseId}", courseId);
            var modules = await moduleRepository.GetModulesByCourseIdAsync(courseId);
            if (modules == null || modules.Count == 0)
            {
                logger.LogWarning("No Modules were found.");
                return new List<ModuleResponseModel>();
            }
            logger.LogInformation("Returning modules for course with ID {courseId}", courseId);
            return modules.Select(m => new ModuleResponseModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                CourseName = m.Course.Title,
                CourseId = m.CourseId,
                Order = m.Order,
                Lessons = m.Lessons.Select(l => new LessonResponseModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Content = l.Content,
                    ModuleName = l.Module.Title,
                    ModuleId = l.ModuleId,
                    Order = l.Order
                }).ToList()
            }).ToList();
        }
    }
}
