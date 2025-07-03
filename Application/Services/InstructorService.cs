namespace Application.Services;

using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _repository;

    public InstructorService(IInstructorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Instructor>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Instructor?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<string> AddAsync(InstructorDto dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
            return "Ya existe un instructor con ese nombre.";

        var instructor = new Instructor { Name = dto.Name };
        await _repository.AddAsync(instructor);
        return "Instructor creado correctamente.";
    }

    public async Task<string> UpdateAsync(int id, InstructorDto dto)
    {
        var instructor = await _repository.GetByIdAsync(id);
        if (instructor == null) return "Instructor no encontrado.";

        if (await _repository.ExistsByNameAsync(dto.Name))
            return "Ya existe un instructor con ese nombre.";

        instructor.Name = dto.Name;
        await _repository.UpdateAsync(instructor);
        return "Instructor actualizado correctamente.";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var instructor = await _repository.GetByIdAsync(id);
        if (instructor == null) return "Instructor no encontrado.";

        if (await _repository.IsLinkedToPublishedCourseAsync(id))
            return "No puedes eliminar un instructor con cursos publicados.";

        await _repository.DeleteAsync(instructor);
        return "Instructor eliminado correctamente.";
    }
}
