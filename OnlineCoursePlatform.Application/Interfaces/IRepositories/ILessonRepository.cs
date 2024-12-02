using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces.IRepositories
{
    public interface ILessonRepository
    {
        Task CreateLessonAsync(Lesson lesson);
        Task<Lesson?> GetLessonByIdAsync(int id);
        Task<List<Lesson>> GetAllLessonsAsync();
        Task<List<Lesson>> GetLessonsByModuleIdAsync(int moduleId);
        Task DeleteLessonAsync(Lesson lesson);
        Task SaveChangesAsync();
    }
}
