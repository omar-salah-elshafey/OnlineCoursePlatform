using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces.IRepositories
{
    public interface IModuleRepository
    {
        Task<Module?> GetModuleByIdAsync(int id);
        Task<List<Module>> GetAllModulesAsync();
        Task AddModuleAsync(Module module);
        Task DeleteModuleAsync(Module module);
        Task SaveChangesAsync();
    }
}
