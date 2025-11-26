using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    //support functions
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? orderBy { get; }

    Expression<Func<T, object>>? orderByDescinding { get; }

    bool IsDistinct { get; }

    int Take { get; }
    int Skip { get; }

    bool IsPagingEnabled { get; }

    IQueryable<T> ApplyCriteria(IQueryable<T> inputQuery);
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}
