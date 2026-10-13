namespace StudService.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Uri { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Lection> IdLections { get; set; } = new List<Lection>();
}
