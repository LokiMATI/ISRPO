namespace StudService.DTO;

public class UpdateLectionDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int Number { get; set; }
    public string? Image { get; set; }
}
