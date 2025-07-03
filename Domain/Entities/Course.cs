namespace Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPublished { get; set; }

    // Relaciones
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;
    public ICollection<Module> Modules { get; set; } = new List<Module>();
}

