using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Infrastructure.Data;

namespace OnlineCoursePlatform.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.Where(c => !c.IsDeleted).Include(c => c.Modules).Include(c => c.Instructor).ToListAsync();
            return courses;
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.Where(c=> !c.IsDeleted).Include(c => c.Modules).Include(c => c.Instructor).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Course>> GetCoursesByInstructorIdAsync(string instructorId)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .Include(c => c.Instructor)
                .Where(c => !c.IsDeleted && c.InstructorId == instructorId)
                .ToListAsync();
        }

        public async Task<List<Course>> SearchCoursesByNameAsync(string keyword)
        {
            return await _context.Courses
                .Where(c => !c.IsDeleted && c.Title.ToLower().Contains(keyword.ToLower()))
                .Include(c => c.Modules)
                .Include(c => c.Instructor)
                .ToListAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            course.IsDeleted = true;
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
