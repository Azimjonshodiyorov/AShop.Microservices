using MongoDB.Bson.Serialization.Attributes;

namespace AShop.Catalog.Domain.Entities;

public class ProductBrand : BaseEntity
{
    [BsonElement("Name")]
    public string Name { get; set; }
}