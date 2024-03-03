using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AShop.Identity.Domain.Entities.Common;

public abstract class BaseEntity<T>
{
    [Key]
    [Column("id")]
    public T Id { get; set; }
}