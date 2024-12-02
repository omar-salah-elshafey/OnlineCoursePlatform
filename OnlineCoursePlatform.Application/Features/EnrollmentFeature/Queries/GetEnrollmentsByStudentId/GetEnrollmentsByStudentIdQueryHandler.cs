using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByStudentId
{
    public class GetEnrollmentsByStudentIdQueryHandler(IEnrollmentRepository enrollmentRepository,
        ILogger<GetEnrollmentsByStudentIdQueryHandler> logger) 
        : IRequestHandler<GetEnrollmentsByStudentIdQuery, List<EnrollmentResponseModel>>
    {
        public async Task<List<EnrollmentResponseModel>> Handle(GetEnrollmentsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching enrollments for student with ID {StudentId}", request.StudentId);

            var enrollments = await enrollmentRepository.GetEnrollmentsByStudentIdAsync(request.StudentId);

            if (enrollments == null || enrollments.Count == 0)
            {
                logger.LogWarning("No Enrollments were found for this Course.");
                return new List<EnrollmentResponseModel>();
            }

            logger.LogInformation("Returning the Enrollments Details...");

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
