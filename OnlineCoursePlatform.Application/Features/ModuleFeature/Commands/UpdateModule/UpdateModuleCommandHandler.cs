using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.UpdateModule
{
    public class UpdateModuleCommandHandler(IModuleRepository moduleRepository, UserManager<User> _userManager,
        ILogger<UpdateModuleCommandHandler> logger, ICourseRepository courseRepository) : IRequestHandler<UpdateModuleCommand, ModuleResponseModel>
    {
        public async Task<ModuleResponseModel> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleId = request.ModuleId;
            var module = await moduleRepository.GetModuleByIdAsync(moduleId);
            if (module == null)
            {
                logger.LogError("Module not Found.");
                return new ModuleResponseModel { Message = "Module not Found." };
            }
            var currentUserId = request.CurrentUserId;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var instructorID = module.Course.InstructorId;
            var isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            if (!instructorID.Equals(currentUserId) && !isAdmin)
            {
                logger.LogError("Unauthorized action.");
                return new ModuleResponseModel { Message = "You are not authorized to Update this Module." };
            }
            var moduleDto = request.UpdateModuleDto;
            module.Title = moduleDto.Title;
            module.Description = moduleDto.Description;
            module.Order = moduleDto.Order;
            await moduleRepository.SaveChangesAsync();
            return new ModuleResponseModel
            {
                Id = moduleId,
                Title = module.Title,
                Description = module.Description,
                Order = module.Order,
                CourseName = module.Course.Title,
                CourseId = module.CourseId,
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
