using System;
using Core.Entities;
namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productdata = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(productdata);
            if (products == null) return;

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }

}
