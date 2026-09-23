using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UserOptimizationExample.Models;

namespace UserOptimizationExample.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        optionsBuilder.UseSqlite(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasCheckConstraint("CK_User_Name_MaxLength", "length(Name) <= 25");
        });
    }
}
