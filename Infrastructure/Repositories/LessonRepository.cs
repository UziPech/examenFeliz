using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _context;

    public LessonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lesson>> GetAllAsync()
        => await _context.Lessons
            .Include(l => l.Module)
            .ToListAsync();

    public async Task<Lesson?> GetByIdAsync(int id)
        => await _context.Lessons
            .Include(l => l.Module)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task AddAsync(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Lesson lesson)
    {
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Lesson lesson)
    {
        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();
    }
}
