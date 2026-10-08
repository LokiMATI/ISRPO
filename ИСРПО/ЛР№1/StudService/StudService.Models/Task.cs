namespace StudService.Models;

public partial class Task
{
    public int Id { get; set; }

    public int LectionId { get; set; }

    public string Text { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Lection Lection { get; set; } = null!;
}
