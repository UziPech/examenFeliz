using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
        => await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Modules)
            .ToListAsync();

    public async Task<Course?> GetByIdAsync(int id)
        => await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Course course)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByTitleAsync(string title)
        => await _context.Courses.AnyAsync(c => c.Title == title);
}
