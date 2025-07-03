
using Domain.Entities;

namespace Domain.Interfaces;

public interface IInstructorRepository
{
    Task<IEnumerable<Instructor>> GetAllAsync();
    Task<Instructor?> GetByIdAsync(int id);
    Task AddAsync(Instructor instructor);
    Task UpdateAsync(Instructor instructor);
    Task DeleteAsync(Instructor instructor);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> IsLinkedToPublishedCourseAsync(int instructorId);
}
