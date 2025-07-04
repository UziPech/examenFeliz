namespace Application.DTOs;

public class LessonDto
{
    public string Title { get; set; } = default!;
    public int ModuleId { get; set; }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
