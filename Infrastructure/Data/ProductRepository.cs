using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public class ProductRepository : IProductsRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }
    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);


    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }



    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {id} not found.");

        return product;
    }


    public async Task<IReadOnlyList<Product>> GetProductByBrandorTypeAsync(
    string? brand, string? type, string? sort = null)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(p => p.Brand == brand);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(p => p.Type == type);

        // Always sort by Name by default
        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            query = sort switch
            {
                "priceasc" => query.OrderBy(p => p.Price),
                "pricedesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
        }
        else
        {
            query = query.OrderBy(p => p.Name);
        }

        return await query.ToListAsync();
    }




    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await _context.Products.Select(x => x.Type).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<String>> GetBrandsAsync()
    {
        return await _context.Products.Select(x => x.Brand).Distinct().ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;

    }
}
