using System.ComponentModel.DataAnnotations.Schema;

namespace AShop.Identity.Domain.Entities.Common;

public class AuditableBaseEntity<T> :BaseEntity<T>
{
    [Column("create_at")] 
    public DateTime CreatedAt { get; init; } 
    [Column("update_at")] 
    public DateTime UpdatedAt { get; set; }
}