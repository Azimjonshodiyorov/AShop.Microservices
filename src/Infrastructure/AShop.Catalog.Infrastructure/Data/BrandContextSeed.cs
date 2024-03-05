using System.Text.Json;
using AShop.Catalog.Domain.Entities;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Data;

public static class BrandContextSeed
{
    public static void SendData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(x => true).Any();
        string path = Path.Combine("Data", "SeedData", "Brands.json");
        if (!checkBrands)
        {
            var brandsData = File.ReadAllText(path);
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands != null)
            {
                foreach (var item in brands)
                {
                    brandCollection.InsertOneAsync(item);
                }
            }
        }
    }
}