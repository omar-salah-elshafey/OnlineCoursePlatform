using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces.IRepositories
{
    public interface IEnrollmentRepository
    {
        Task CreateEnrollmentAsync(Enrollment enrollment);
        Task DeleteEnrollmentAsync(Enrollment enrollment);
        Task<Enrollment?> GetEnrollmentByIdAsync(int enrollmentId);
        Task<List<Enrollment>> GetAllEnrollmentsAsync();
        Task SaveChangesAsync();
        Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(string studentId, int courseId);
        Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(string studentId);
        Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);

    }
}
