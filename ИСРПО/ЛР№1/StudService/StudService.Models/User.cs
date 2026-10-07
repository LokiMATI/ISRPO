namespace StudService.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Role Role { get; set; } = null!;
}
