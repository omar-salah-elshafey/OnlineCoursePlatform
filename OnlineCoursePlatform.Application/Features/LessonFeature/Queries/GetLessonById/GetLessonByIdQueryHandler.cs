using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonById
{
    public class GetLessonByIdQueryHandler(ILessonRepository lessonRepository, IModuleRepository moduleRepository,
        ILogger<GetLessonByIdQueryHandler> logger) : IRequestHandler<GetLessonByIdQuery, LessonResponseModel>
    {
        public async Task<LessonResponseModel> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonId = request.Id;
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            if (lesson == null)
            {
                logger.LogError("Lesson not found.");
                return new LessonResponseModel { Message = "Lesson not found." };
            }
            logger.LogInformation("Returning lesson details.");
            return new LessonResponseModel
            {
                Id = lessonId,
                Title = lesson.Title,
                Content = lesson.Content,
                ModuleName = lesson.Module.Title,
                ModuleId = lesson.ModuleId,
                Order = lesson.Order,
            };
        }
    }
}
