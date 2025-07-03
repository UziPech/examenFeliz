using Domain.Entities;

namespace Domain.Interfaces;

public interface IModuleRepository
{
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module?> GetByIdAsync(int id);
    Task AddAsync(Module module);
    Task UpdateAsync(Module module);
    Task DeleteAsync(Module module);
}
