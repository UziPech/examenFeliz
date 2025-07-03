using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Course?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    public async Task<string> AddAsync(CourseDto dto)
    {
        if (await _repository.ExistsByTitleAsync(dto.Title))
            return "Ya existe un curso con ese título.";

        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            IsPublished = dto.IsPublished,
            InstructorId = dto.InstructorId
        };

        await _repository.AddAsync(course);
        return "Curso creado correctamente.";
    }

    public async Task<string> UpdateAsync(int id, CourseDto dto)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return "Curso no encontrado.";

        if (await _repository.ExistsByTitleAsync(dto.Title))
            return "Ya existe un curso con ese título.";

        course.Title = dto.Title;
        course.Description = dto.Description;
        course.IsPublished = dto.IsPublished;
        course.InstructorId = dto.InstructorId;

        await _repository.UpdateAsync(course);
        return "Curso actualizado correctamente.";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return "Curso no encontrado.";

        await _repository.DeleteAsync(course);
        return "Curso eliminado correctamente.";
    }
}
