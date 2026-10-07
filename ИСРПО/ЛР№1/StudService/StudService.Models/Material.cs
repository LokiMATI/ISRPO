namespace StudService.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Uri { get; set; } = null!;

    public string Type { get; set; } = null!;
}
