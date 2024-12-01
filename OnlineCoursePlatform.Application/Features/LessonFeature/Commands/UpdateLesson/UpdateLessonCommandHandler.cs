using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler(ILessonRepository lessonRepository, IModuleRepository moduleRepository,
        ILogger<UpdateLessonCommandHandler> logger) : IRequestHandler<UpdateLessonCommand, LessonResponseModel>
    {
        public async Task<LessonResponseModel> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonId = request.Id;
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            if (lesson == null)
            {
                logger.LogError("Lesson not found.");
                return new LessonResponseModel { Message = "Lesson not found." };
            }
            var lessonDto = request.LessonDto;
            lesson.Title = lessonDto.Title;
            lesson.Content = lessonDto.Content;
            lesson.ModuleId = lessonDto.ModuleId;
            lesson.Order = lessonDto.Order;
            await lessonRepository.SaveChangesAsync();
            logger.LogInformation($"Lesson with Id: {lessonId} has beed Updated.");
            return new LessonResponseModel
            {
                Id = lessonId,
                Title = lesson.Title,
                Content = lesson.Content,
                ModuleName = lesson.Module.Title,
                Order = lesson.Order,
            };
        }
    }
}
