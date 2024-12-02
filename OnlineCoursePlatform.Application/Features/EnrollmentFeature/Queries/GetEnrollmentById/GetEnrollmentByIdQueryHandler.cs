using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentById
{
    public class GetEnrollmentByIdQueryHandler(IEnrollmentRepository _enrollmentRepository, ILogger<GetEnrollmentByIdQueryHandler> logger)
        : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentResponseModel>
    {
        public async Task<EnrollmentResponseModel> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Attemping to to get the Enrollment with ID: {request.EnrollmentId}.");
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId);
            if (enrollment == null)
            {
                logger.LogError("Enrollment not Found.");
                return new EnrollmentResponseModel { Message = "Enrollment not Found." };
            }
            logger.LogInformation($"Returnig the Enrollment with ID: {request.EnrollmentId}.");
            return new EnrollmentResponseModel
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
            };
        }
    }
}
