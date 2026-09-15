using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication.Models;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    public string Email;

    public List<Order> Orders { get; set; }

    public string GetName() => "Customer: " + Name;

    public string GetEmail() => "Customer Email: " + Email;
}
