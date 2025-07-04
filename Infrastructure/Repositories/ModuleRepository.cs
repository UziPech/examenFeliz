using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly AppDbContext _context;

    public ModuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules
            .Include(m => m.Course)
            .Include(m => m.Lessons)
            .ToListAsync();
    }

    public async Task<Module?> GetByIdAsync(int id)
    {
        return await _context.Modules
            .Include(m => m.Course)
            .Include(m => m.Lessons)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Module module)
    {
        await _context.Modules.AddAsync(module);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Module module)
    {
        _context.Modules.Update(module);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Module module)
    {
        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
    }
}
