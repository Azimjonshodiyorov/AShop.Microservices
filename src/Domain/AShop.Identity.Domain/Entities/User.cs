using System.ComponentModel.DataAnnotations.Schema;
using AShop.Identity.Domain.Entities.Common;

namespace AShop.Identity.Domain.Entities;

public class User : AuditableBaseEntity<long>
{
    [Column("first_name")] 
    public string FirstName { get; set; }
    [Column("last_name")] 
    public string? LastName { get; set; }
    [Column("date_of_brith")]
    public DateTimeOffset DateOfBrith { get; set; }
    [Column("phone_number")] 
    public string PhoneNumber { get; set; }
    [Column("email")] 
    public string Email { get; set; }
    [Column("paswword")]
    public string Password { get; set; }
    [Column("is_bloked")] 
    public bool IsBlocked { get; set; } = false;
}