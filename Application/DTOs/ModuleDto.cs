namespace Application.DTOs;

public class ModuleDto
{
    public string Title { get; set; } = default!;
    public int CourseId { get; set; }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
