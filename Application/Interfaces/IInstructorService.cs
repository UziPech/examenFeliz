namespace Application.Interfaces;

using Application.DTOs;
using Domain.Entities;

public interface IInstructorService
{
    Task<IEnumerable<Instructor>> GetAllAsync();
    Task<Instructor?> GetByIdAsync(int id);
    Task<string> AddAsync(InstructorDto dto);
    Task<string> UpdateAsync(int id, InstructorDto dto);
    Task<string> DeleteAsync(int id);
}
