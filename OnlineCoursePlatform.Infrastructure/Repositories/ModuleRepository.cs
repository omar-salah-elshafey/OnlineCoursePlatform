using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Infrastructure.Data;

namespace OnlineCoursePlatform.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddModuleAsync(Module module)
        {
            await _context.AddAsync(module);
        }

        public async Task DeleteModuleAsync(Module module)
        {
            module.IsDeleted = true;
            _context.SaveChanges();
        }

        public async Task<List<Module>> GetAllModulesAsync()
        {
            return await _context.Modules.Where(m => !m.IsDeleted).Include(m => m.Lessons).ToListAsync();
        }

        public async Task<Module?> GetModuleByIdAsync(int id)
        {
            return await _context.Modules.Where(m => !m.IsDeleted).Include(m => m.Lessons).FirstOrDefaultAsync(m => id == m.Id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
