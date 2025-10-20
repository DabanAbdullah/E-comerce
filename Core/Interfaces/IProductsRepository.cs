using System;
using Core.Entities;
namespace Core.Interfaces;


public interface IProductsRepository
{

    public Task<IReadOnlyList<Product>> GetProductsAsync();

    public Task<Product> GetProductByIdAsync(int id);

    public void CreateProduct(Product product);

    public void UpdateProduct(Product product);

    public void DeleteProduct(Product product);

    Task<bool> SaveChangesAsync();

}
