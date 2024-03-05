using System.Text.Json;
using AShop.Catalog.Domain.Entities;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Data;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProduct = productCollection.Find(x => true).Any();
        string path = Path.Combine("Data", "SeedData", "Product.json");
        if (!checkProduct)
        {
            var productsData =  File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}