using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Models;

namespace WellnessMassageCenter.Backend.DAL.Contexts;

public partial class DbWellnessMassageCenterContext : DbContext
{
    public DbWellnessMassageCenterContext()
    {
    }

    public DbWellnessMassageCenterContext(DbContextOptions<DbWellnessMassageCenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("postition_fk");

            entity.HasMany(d => d.Services).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "ServicesWorker",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employees_has_services_services1"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employees_has_services_employees1"),
                    j =>
                    {
                        j.HasKey("EmployeesId", "ServicesId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("services_workers");
                        j.HasIndex(["EmployeesId"], "fk_employees_has_services_employees1_idx");
                        j.HasIndex(["ServicesId"], "fk_employees_has_services_services1_idx");
                        j.IndexerProperty<int>("EmployeesId").HasColumnName("employees_id");
                        j.IndexerProperty<int>("ServicesId").HasColumnName("services_id");
                    });
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_services_categories1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
