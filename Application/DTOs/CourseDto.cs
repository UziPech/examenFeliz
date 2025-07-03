namespace Application.DTOs;

public class CourseDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPublished { get; set; }
    public int InstructorId { get; set; }
}
