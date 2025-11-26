using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);

    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public void AddAsync(T entity)
    {
        context.Set<T>().AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(e => e.Id == id);
    }

    public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        try
        {
            // Print generated SQL for debugging (helpful to diagnose SQL syntax errors)
            Console.WriteLine("Generated SQL:\n" + query.ToQueryString());
        }
        catch
        {
            // Ignore if ToQueryString is not supported in the runtime environment
        }

        try
        {
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            try
            {
                throw new Exception($"Error executing SQL:\n{query.ToQueryString()}", ex);
            }
            catch
            {
                throw;
            }
        }
    }




    public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetListWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        var query = ApplySpecification(spec);
        try
        {
            Console.WriteLine("Generated SQL (select):\n" + query.ToQueryString());
        }
        catch
        {
        }

        try
        {
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            try
            {
                throw new Exception($"Error executing SQL (select):\n{query.ToQueryString()}", ex);
            }
            catch
            {
                throw;
            }
        }
    }


    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(context.Set<T>().AsQueryable(), spec);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }
}
