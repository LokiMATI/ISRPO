using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WellnessMassageCenter.Backend.DAL.Models;

/// <summary>
/// Сотрудник
/// </summary>
[Table("employees")]
[Index("PositionId", Name = "postition_fk_idx")]
public partial class Employee
{
    /// <summary>
    /// Идентификатро
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Специализация
    /// </summary>
    [Column("specialization")]
    [StringLength(255)]
    public string Specialization { get; set; } = null!;

    /// <summary>
    /// Идентификатор должности
    /// </summary>
    [Column("position_id")]
    public int PositionId { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    [Column("rating")]
    [Precision(10, 0)]
    public decimal Rating { get; set; }

    /// <summary>
    /// Путь к файлу аватарки сотрудника
    /// </summary>
    [Column("avatar")]
    [StringLength(1000)]
    public string Avatar { get; set; } = null!;

    /// <summary>
    /// Путь к файлу аватарки сотрудника в более высоком разрешении
    /// </summary>
    [Column("avatar_big")]
    [StringLength(1000)]
    public string AvatarBig { get; set; } = null!;

    /// <summary>
    /// Дополнительная информация о сотруднике (HTML формат)
    /// </summary>
    [Column("information", TypeName = "text")]
    public string Information { get; set; } = null!;

    /// <summary>
    /// Скрыт ли от онлайн записи
    /// </summary>
    [Column("hidden")]
    public bool IsHidden { get; set; }

    /// <summary>
    /// Должность
    /// </summary>
    [ForeignKey("PositionId")]
    [InverseProperty("Employees")]
    public virtual Position Position { get; set; } = null!;

    /// <summary>
    /// Услуги
    /// </summary>
    [ForeignKey("EmployeesId")]
    [InverseProperty("Employees")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
