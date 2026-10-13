namespace StudService.Models;

public partial class Answer
{
    public int UserId { get; set; }

    public int TaskId { get; set; }

    public int Answer1 { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
