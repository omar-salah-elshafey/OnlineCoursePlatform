using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Infrastructure.Data;

namespace OnlineCoursePlatform.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateLessonAsync(Lesson lesson)
        {
            await _context.AddAsync(lesson);
        }

        public async Task DeleteLessonAsync(Lesson lesson)
        {
            lesson.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.Where(l => !l.IsDeleted && !l.Module.IsDeleted && !l.Module.Course.IsDeleted)
                .Include(l => l.Module)
                .ThenInclude(m => m.Course)
                .ToListAsync();
        }

        public async Task<Lesson?> GetLessonByIdAsync(int id)
        {
            return await _context.Lessons.Where(l => !l.IsDeleted && !l.Module.IsDeleted && !l.Module.Course.IsDeleted)
                .Include(l => l.Module)
                .ThenInclude(m  => m.Course)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
