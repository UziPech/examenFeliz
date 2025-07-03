using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IModuleService
{
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module?> GetByIdAsync(int id);
    Task<string> CreateAsync(ModuleDto dto);
    Task<string> UpdateAsync(int id, ModuleDto dto);
    Task<string> DeleteAsync(int id);
}
