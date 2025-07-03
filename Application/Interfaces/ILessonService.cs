using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson?> GetByIdAsync(int id);
    Task<string> CreateAsync(LessonDto dto);
    Task<string> UpdateAsync(int id, LessonDto dto);
    Task<string> DeleteAsync(int id);
}
