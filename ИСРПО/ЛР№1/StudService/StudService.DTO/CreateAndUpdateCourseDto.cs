namespace StudService.DTO;

public class CreateAndUpdateCourseDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
}
