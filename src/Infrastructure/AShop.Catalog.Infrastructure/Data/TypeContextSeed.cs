using System.Text.Json;
using AShop.Catalog.Domain.Entities;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Data;

public static class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        var checkTypes = typeCollection.Find(x => true).Any();
        string path = Path.Combine("Data", "SeedData", "Types.json");
        if (!checkTypes)
        {
            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            foreach (var type in types)
            {
                typeCollection.InsertOneAsync(type);
            }
        }
    }
}