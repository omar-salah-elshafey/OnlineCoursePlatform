﻿using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.CreateModule
{
    public class CreateModuleCommandHandler(IModuleRepository moduleRepository, ILogger<CreateModuleCommandHandler> logger, 
        ICourseRepository courseRepository) : IRequestHandler<CreateModuleCommand, ModuleResponseModel>
    {
        public async Task<ModuleResponseModel> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleDto = request.ModuleDto;
            var courseId = moduleDto.CourseId;
            var course = await courseRepository.GetCourseByIdAsync(courseId);
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
            logger.LogInformation($"Module Created Successfully {Environment.NewLine}Returning Module Details...");

            return new ModuleResponseModel
            {
                Id = module.Id,
                Title = moduleDto.Title,
                Description = moduleDto.Description,
                Order = moduleDto.order,
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
