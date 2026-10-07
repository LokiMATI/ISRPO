namespace StudService.Models;

public partial class Lection
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int CourseId { get; set; }

    public int Number { get; set; }

    public string? Image { get; set; }
}
