namespace StudService.Models;

public partial class Lection
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int CourseId { get; set; }

    public int Number { get; set; }

    public string? Image { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Material> IdMaterials { get; set; } = new List<Material>();
}
