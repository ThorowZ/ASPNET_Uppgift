using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(StatusName), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int id { get; set; }
    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}