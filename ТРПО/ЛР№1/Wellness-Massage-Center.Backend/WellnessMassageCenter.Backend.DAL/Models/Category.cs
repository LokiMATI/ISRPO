using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellnessMassageCenter.Backend.DAL.Models;

/// <summary>
/// Категория услуг
/// </summary>
[Table("categories")]
public partial class Category
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Услуги
    /// </summary>
    [InverseProperty("Category")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
