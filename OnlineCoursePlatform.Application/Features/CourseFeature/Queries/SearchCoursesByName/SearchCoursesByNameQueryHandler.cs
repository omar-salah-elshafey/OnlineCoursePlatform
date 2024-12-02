using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.CourseFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Queries.SearchCoursesByName
{
    public class SearchCoursesByNameQueryHandler(ICourseRepository courseRepository, ILogger<SearchCoursesByNameQueryHandler> logger)
    : IRequestHandler<SearchCoursesByNameQuery, List<CourseResponseModel>>
    {
        public async Task<List<CourseResponseModel>> Handle(SearchCoursesByNameQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword;
            logger.LogInformation("Searching courses with keyword: {Keyword}", keyword);
            var courses = await courseRepository.SearchCoursesByNameAsync(keyword);
            if (courses.Count == 0)
            {
                logger.LogWarning("No courses found with keyword: {Keyword}", keyword);
                return new List<CourseResponseModel>();
            }
            return courses.Select(c => new CourseResponseModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsPublished = c.IsPublished,
                Price = c.Price,
                ThumbnailUrl = c.ThumbnailUrl,
                InstructorId = c.InstructorId,
                InstructorName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
            }).ToList();
        }
    }
}
