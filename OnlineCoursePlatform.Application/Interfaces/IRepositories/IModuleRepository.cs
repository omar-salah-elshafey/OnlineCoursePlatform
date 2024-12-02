using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces.IRepositories
{
    public interface IModuleRepository
    {
        Task AddModuleAsync(Module module);
        Task<List<Module>> GetAllModulesAsync();
        Task<Module?> GetModuleByIdAsync(int id);
        Task<List<Module>> GetModulesByCourseIdAsync(int courseId);
        Task DeleteModuleAsync(Module module);
        Task SaveChangesAsync();
    }
}
