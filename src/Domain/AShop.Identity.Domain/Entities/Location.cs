using System.ComponentModel.DataAnnotations.Schema;

namespace AShop.Identity.Domain.Entities;

public class Location
{
    [Column("state")]
    public string State { get; set; }
    [Column("city")]
    public string City { get; set; }
    [Column("street")]
    public string Street { get; set; }
    [Column("zipcode")]
    public string Zipcode { get; set; }
}