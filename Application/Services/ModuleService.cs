using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _repository;

    public ModuleService(IModuleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Module?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    public async Task<string> CreateAsync(ModuleDto dto)
    {
        var module = new Module
        {
            Title = dto.Title,
            CourseId = dto.CourseId
        };

        await _repository.AddAsync(module);
        return "Módulo creado correctamente.";
    }

    public async Task<string> UpdateAsync(int id, ModuleDto dto)
    {
        var module = await _repository.GetByIdAsync(id);
        if (module == null) return "Módulo no encontrado.";

        module.Title = dto.Title;
        module.CourseId = dto.CourseId;
        await _repository.UpdateAsync(module);
        return "Módulo actualizado.";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var module = await _repository.GetByIdAsync(id);
        if (module == null) return "Módulo no encontrado.";

        await _repository.DeleteAsync(module);
        return "Módulo eliminado.";
    }
}
