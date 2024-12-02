using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModuleById
{
    public class GetModuleByIdQueryHandler(IModuleRepository moduleRepository, ICourseRepository courseRepository,
        ILogger<GetModuleByIdQueryHandler> logger) : IRequestHandler<GetModuleByIdQuery, ModuleResponseModel>
    {
        public async Task<ModuleResponseModel> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting Searching for the Module...");
            var moduleId = request.ModuleId;
            var module = await moduleRepository.GetModuleByIdAsync(moduleId);
            if (module == null)
            {
                logger.LogError($"Module with Id: {moduleId} is not found.");
                return new ModuleResponseModel { Message = $"Module with Id: {moduleId} is not found." };
            }
            logger.LogInformation("Returning Module Details....");
            return new ModuleResponseModel
            {
                Id = moduleId,
                Title = module.Title,
                Description = module.Description,
                CourseName = module.Course.Title,
                CourseId = module.CourseId,
                Order = module.Order,
                Lessons = module.Lessons.Select(l => new LessonResponseModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Content = l.Content,
                    ModuleName = l.Module.Title,
                    ModuleId = l.ModuleId,
                    Order = l.Order
                }).ToList()
            };
        }
    }
}
