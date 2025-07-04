namespace Domain.Entities;

public class Module
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int CourseId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Relaciones
    public Course? Course { get; set; }
    public ICollection<Lesson>? Lessons { get; set; }
}
