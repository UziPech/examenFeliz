using Domain.Entities;
using Domain.Interfaces; // ← ESTE ES EL IMPORTANTE
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InstructorRepository : IInstructorRepository
{
    private readonly AppDbContext _context;

    public InstructorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Instructor>> GetAllAsync()
    {
        return await _context.Instructors.ToListAsync();
    }

    public async Task<Instructor?> GetByIdAsync(int id)
    {
        return await _context.Instructors.FindAsync(id);
    }

    public async Task AddAsync(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Instructor instructor)
    {
        _context.Instructors.Update(instructor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Instructor instructor)
    {
        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Instructors.AnyAsync(i => i.Name == name);
    }

    public async Task<bool> IsLinkedToPublishedCourseAsync(int instructorId)
    {
        return await _context.Courses.AnyAsync(c => c.InstructorId == instructorId && c.IsPublished);
    }
}
