using Microsoft.EntityFrameworkCore;
using StudService.Models;

namespace StudService.EntityFramework.Contexts;

public partial class StudDbContext : DbContext
{
    public StudDbContext()
    {
    }

    public StudDbContext(DbContextOptions<StudDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lection> Lections { get; set; }

    public virtual DbSet<LectionsMaterial> LectionsMaterials { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }
    
    public virtual DbSet<Models.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(@"Data Source=C:\Temp\ispp31\ISRPO\ИСРПО\ЛР№1\Database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.UserId });

            entity.Property(e => e.TaskId).HasColumnName("Task_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Answer1).HasColumnName("Answer");

            entity.HasOne(d => d.Task).WithMany(p => p.Answers)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("TEXT(500)");
            entity.Property(e => e.Image).HasColumnType("TEXT(1000)");
            entity.Property(e => e.Title).HasColumnType("TEXT(255)");
        });

        modelBuilder.Entity<Lection>(entity =>
        {
            entity.Property(e => e.Content).HasColumnType("TEXT(15000)");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Title).HasColumnType("TEXT(255)");
        });

        modelBuilder.Entity<LectionsMaterial>(entity =>
        {
            entity.HasKey(e => new { e.IdLection, e.IdMaterial });

            entity.ToTable("Lections_Materials");

            entity.Property(e => e.IdLection).HasColumnName("Id_Lection");
            entity.Property(e => e.IdMaterial).HasColumnName("Id_Material");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.Property(e => e.Type).HasColumnType("TEXT(30)");
            entity.Property(e => e.Uri).HasColumnType("TEXT(1000)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Title).HasColumnType("TEXT(255)");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.Property(e => e.Answer).HasColumnType("TEXT(1000)");
            entity.Property(e => e.LectionId).HasColumnName("Lection_Id");
            entity.Property(e => e.Text).HasColumnType("TEXT(1000)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.RoleId)
                .HasDefaultValue(1)
                .HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
