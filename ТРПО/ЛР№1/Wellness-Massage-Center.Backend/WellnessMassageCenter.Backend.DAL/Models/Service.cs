using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WellnessMassageCenter.Backend.DAL.Models;

/// <summary>
/// Услуга
/// </summary>
[Table("services")]
[Index("CategoryId", Name = "fk_services_categories1_idx")]
public partial class Service
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор категории услуги
    /// </summary>
    [Column("category_id")]
    public int CategoryId { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Комментарий
    /// </summary>
    [Column("comment")]
    [StringLength(5000)]
    public string Comment { get; set; } = null!;

    /// <summary>
    /// Продолжительность
    /// </summary>
    [Column("duration")]
    public short Duration { get; set; }

    /// <summary>
    /// Возможна ли регистрация онлайн
    /// </summary>
    [Column("service_type")]
    public bool IsCanRegisteredOnline { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    [ForeignKey("CategoryId")]
    [InverseProperty("Services")]
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Персонал
    /// </summary>
    [ForeignKey("ServicesId")]
    [InverseProperty("Services")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
