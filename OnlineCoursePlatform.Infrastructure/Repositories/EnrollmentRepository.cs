using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Infrastructure.Data;

namespace OnlineCoursePlatform.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEnrollmentAsync(Enrollment enrollment)
        {
            await _context.AddAsync(enrollment);
        }

        public async Task DeleteEnrollmentAsync(Enrollment enrollment)
        {
            enrollment.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollments.Where(e => !e.IsDeleted && !e.Student.IsDeleted && !e.Course.IsDeleted)
                .Include(e => e.Student).Include(e => e.Course).ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int enrollmentId)
        {
            return await _context.Enrollments.Where(e => !e.IsDeleted && !e.Student.IsDeleted && !e.Course.IsDeleted)
                .Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync(e => e.Id == enrollmentId);
        }

        public async Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(string studentId, int courseId)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId && !e.IsDeleted);
        }

        public async Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(string studentId)
        {
            return await _context.Enrollments
                .Where(e => !e.IsDeleted && e.StudentId == studentId && !e.Student.IsDeleted && !e.Course.IsDeleted)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _context.Enrollments
                .Where(e => !e.IsDeleted && e.CourseId == courseId && !e.Student.IsDeleted && !e.Course.IsDeleted)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
