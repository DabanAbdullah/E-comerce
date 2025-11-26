using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{

    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);

    Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec);


    Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec);

    Task<IReadOnlyList<TResult>> GetListWithSpecAsync<TResult>(ISpecification<T, TResult> spec);


    void AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);

    Task<bool> SaveAllAsync();

    bool Exists(int id);

    Task<int> CountAsync(ISpecification<T> spec);
}

