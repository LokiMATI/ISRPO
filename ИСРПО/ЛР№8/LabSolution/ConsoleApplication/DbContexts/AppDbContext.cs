using ConsoleApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=OrderManagement.db");
    }
}
