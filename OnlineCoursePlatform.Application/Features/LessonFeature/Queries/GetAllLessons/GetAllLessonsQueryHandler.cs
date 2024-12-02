using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetAllLessons
{
    public class GetAllLessonsQueryHandler(ILessonRepository lessonRepository, IModuleRepository moduleRepository,
        ILogger<GetAllLessonsQueryHandler> logger) : IRequestHandler<GetAllLessonsQuery, List<LessonResponseModel>>
    {
        public async Task<List<LessonResponseModel>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            var lessons = await lessonRepository.GetAllLessonsAsync();
            if (lessons == null || !lessons.Any())
            {
                logger.LogWarning("No Lessons were found.");
                return new List<LessonResponseModel>();
            }
            var lessonResponseModel = new List<LessonResponseModel>();
            foreach (var lesson in lessons)
            {
                lessonResponseModel.Add(new LessonResponseModel
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    Order = lesson.Order,
                    ModuleName = lesson.Module.Title,
                    ModuleId = lesson.ModuleId
                });
            }
            logger.LogInformation("Returning Lessons Details.");
            return lessonResponseModel;
        }
    }
}
