namespace Domain.Entities;

public class Instructor
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!; // ✅ Propiedad que faltaba

    // Relación con cursos
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
