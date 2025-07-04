using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<string> AddAsync(CourseDto dto);
    Task<string> UpdateAsync(int id, CourseDto dto);
    Task<string> DeleteAsync(int id);
}
