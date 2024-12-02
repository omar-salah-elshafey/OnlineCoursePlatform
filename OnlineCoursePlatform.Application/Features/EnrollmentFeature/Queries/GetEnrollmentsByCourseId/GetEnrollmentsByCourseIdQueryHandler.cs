using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByCourseId
{
    public class GetEnrollmentsByCourseIdQueryHandler(IEnrollmentRepository _enrollmentRepository,
        ILogger<GetEnrollmentsByCourseIdQueryHandler> _logger) 
        : IRequestHandler<GetEnrollmentsByCourseIdQuery, List<EnrollmentResponseModel>>
    {
        public async Task<List<EnrollmentResponseModel>> Handle(GetEnrollmentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching enrollments for Course with ID {CourseId}", request.CourseId);
            var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(request.CourseId);
            if (enrollments == null || enrollments.Count == 0)
            {
                _logger.LogWarning("No Enrollments were found for this Course.");
                return new List<EnrollmentResponseModel>();
            }

            _logger.LogInformation("Returning the Enrollments Details...");
            return enrollments.Select(e => new EnrollmentResponseModel
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentName = $"{e.Student.FirstName} {e.Student.LastName}",
                CourseId = e.Course.Id,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Progress = e.Progress,
                IsCompleted = e.IsCompleted
            }).ToList();
        }
    }
}
