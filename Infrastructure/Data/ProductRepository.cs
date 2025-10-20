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

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
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
