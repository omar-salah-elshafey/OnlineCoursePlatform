using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.CreateModule
{
    public class CreateModuleCommandHandler(IModuleRepository moduleRepository, ILogger<CreateModuleCommandHandler> logger)
        : IRequestHandler<CreateModuleCommand, ModuleResponseModel>
    {
        public async Task<ModuleResponseModel> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleDto = request.ModuleDto;
            var courseId = moduleDto.CourseId;
            var course = await moduleRepository.GetModuleByIdAsync(courseId);
            if (course is null)
            {
                logger.LogError("The Course ID is not valid");
                return new ModuleResponseModel { Message = "The Course ID is not valid" };
            }
            var module = new Module
            {
                Title = moduleDto.Title,
                Description = moduleDto.Description,
                CourseId = courseId,
                Order = moduleDto.order,
            };
            
            await moduleRepository.AddModuleAsync(module);
            await moduleRepository.SaveChangesAsync();

            return new ModuleResponseModel
            {
                Id = module.Id,
                Title = moduleDto.Title,
                Description = moduleDto.Description,
                Order = moduleDto.order,
                CourseName = module.Course.Title
            };
        }
    }
}
