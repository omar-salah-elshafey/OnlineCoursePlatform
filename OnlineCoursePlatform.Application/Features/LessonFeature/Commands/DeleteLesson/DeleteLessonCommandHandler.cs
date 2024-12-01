using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler(ILessonRepository lessonRepository, ILogger<DeleteLessonCommandHandler> logger) 
        : IRequestHandler<DeleteLessonCommand, DeleteResponseModel>
    {
        public async Task<DeleteResponseModel> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonId = request.Id;
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            if (lesson == null)
            {
                logger.LogError("Lesson not found.");
                return new DeleteResponseModel { IsDeleted = false, Message = "Lesson not found." };
            }
            await lessonRepository.DeleteLessonAsync(lesson);
            logger.LogInformation("Lesson deleted successfully.");
            return new DeleteResponseModel { IsDeleted = true, Message = "Lesson Has been Deleted Succeessfully." };
        }
    }
}
