using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;

    public LessonService(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LessonDto>> GetAllAsync()
        => (await _repository.GetAllAsync())
            .Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                ModuleId = l.ModuleId,
                CreatedAt = l.CreatedAt
            })
            .ToList();

    public async Task<Lesson?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    public async Task<string> CreateAsync(LessonDto dto)
    {
        var lesson = new Lesson
        {
            Title = dto.Title,
            ModuleId = dto.ModuleId
        };

        await _repository.AddAsync(lesson);
        return "Lección creada correctamente.";
    }

    public async Task<string> UpdateAsync(int id, LessonDto dto)
    {
        var lesson = await _repository.GetByIdAsync(id);
        if (lesson == null) return "Lección no encontrada.";

        lesson.Title = dto.Title;
        lesson.ModuleId = dto.ModuleId;
        await _repository.UpdateAsync(lesson);
        return "Lección actualizada.";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var lesson = await _repository.GetByIdAsync(id);
        if (lesson == null) return "Lección no encontrada.";

        await _repository.DeleteAsync(lesson);
        return "Lección eliminada.";
    }
}
