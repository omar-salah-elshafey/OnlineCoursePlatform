using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonsByModuleId
{
    public class GetLessonsByModuleIdQueryHandler(ILessonRepository _lessonRepository, ILogger<GetLessonsByModuleIdQueryHandler> _logger)
        : IRequestHandler<GetLessonsByModuleIdQuery, List<LessonResponseModel>>
    {
        public async Task<List<LessonResponseModel>> Handle(GetLessonsByModuleIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching lessons for module with ID {ModuleId}", request.ModuleId);

            var lessons = await _lessonRepository.GetLessonsByModuleIdAsync(request.ModuleId);

            if (lessons == null || lessons.Count == 0)
            {
                _logger.LogWarning("No Lessons were found.");
                return new List<LessonResponseModel>();
            }

            _logger.LogInformation("Returning lessons for module with ID {ModuleId}", request.ModuleId);
            return lessons.Select(l => new LessonResponseModel
            {
                Id = l.Id,
                Title = l.Title,
                Content = l.Content,
                ModuleName = l.Module.Title,
                ModuleId = l.ModuleId,
                Order = l.Order,
            }).ToList();
        }
    }
}
