using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Reglas adicionales
        modelBuilder.Entity<Instructor>()
            .HasIndex(i => i.Name)
            .IsUnique();

        modelBuilder.Entity<Instructor>()
            .HasIndex(i => i.Email)
            .IsUnique();
    }
}
