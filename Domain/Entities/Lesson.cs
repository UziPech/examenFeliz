namespace Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int ModuleId { get; set; }

    // Navegación
    public Module? Module { get; set; }
}

