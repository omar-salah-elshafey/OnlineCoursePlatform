using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.CreateLesson
{
    public class CreateLessonCommandHandler(ILessonRepository lessonRepository, ILogger<CreateLessonCommandHandler> logger,
        IModuleRepository moduleRepository) 
        : IRequestHandler<CreateLessonCommand, LessonResponseModel>
    {
        public async Task<LessonResponseModel> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonDto = request.LessonDto;
            var moduleId = lessonDto.ModuleId;
            var module = await moduleRepository.GetModuleByIdAsync(moduleId);
            if (module == null)
            {
                logger.LogError("The Module ID is not valid");
                return new LessonResponseModel { Message = "The Module ID is not valid" };
            }
            var lesson = new Lesson
            {
                Title = lessonDto.Title,
                Content = lessonDto.Content,
                ModuleId = moduleId,
                Order = lessonDto.Order,
            };

            await lessonRepository.CreateLessonAsync(lesson);
            await lessonRepository.SaveChangesAsync();
            logger.LogInformation("Lesson Created Successfully.");
            return new LessonResponseModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Content = lesson.Content,
                ModuleName = lesson.Module.Title,
                ModuleId = lesson.ModuleId,
                Order = lesson.Order
            };
        }
    }
}
