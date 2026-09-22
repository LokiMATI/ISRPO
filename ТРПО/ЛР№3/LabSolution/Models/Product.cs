using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    public int CategoryId { get; set; }

    [Required]
    public string Description { get; set; } = "";
    public double Price { get; set; }
}
