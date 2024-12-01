using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler(ICourseRepository _courseRepository, ILogger<DeleteCourseCommandHandler> _logger)
        : IRequestHandler<DeleteCourseCommand, bool>
    {
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetCourseByIdAsync(request.Id);
            if (course == null)
            {
                _logger.LogError("Course Not Found.");
                return false;
            }

            await _courseRepository.DeleteCourseAsync(course);
            await _courseRepository.SaveChangesAsync();
            _logger.LogInformation("Course Has Been Deleted.");
            return true;
        }
    }
}
