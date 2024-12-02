using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetAllEnrollments
{
    public class GetAllEnrollmentsQueryHandler(IEnrollmentRepository _enrollmentRepository, ILogger<GetAllEnrollmentsQueryHandler> logger)
        : IRequestHandler<GetAllEnrollmentsQuery, List<EnrollmentResponseModel>>
    {
        public async Task<List<EnrollmentResponseModel>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attemping to get all enrollments...");
            var enrollments = await _enrollmentRepository.GetAllEnrollmentsAsync();
            if (enrollments == null || !enrollments.Any())
            {
                logger.LogWarning("No Enrollments were found.");
                return new List<EnrollmentResponseModel>();
            }
            logger.LogInformation("Returning all Enrollments.");
            return enrollments.Select(enrollment => new EnrollmentResponseModel
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = $"{enrollment.Student.FirstName} {enrollment.Student.LastName}",
                CourseId = enrollment.Course.Id,
                CourseTitle = enrollment.Course.Title,
                EnrollmentDate = enrollment.EnrollmentDate,
                Progress = enrollment.Progress,
                IsCompleted = enrollment.IsCompleted,
                CompletionDate = enrollment.CompletionDate
            }).ToList();
        }
    }
}
