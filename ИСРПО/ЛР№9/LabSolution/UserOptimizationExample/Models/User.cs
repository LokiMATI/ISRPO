using System.ComponentModel.DataAnnotations;

namespace UserOptimizationExample.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}
