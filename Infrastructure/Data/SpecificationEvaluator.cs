using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{

    public static IQueryable<T> GetQuery(IQueryable<T> Query, ISpecification<T> spec)
    {
        var query = Query;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);  //(x=?x=>x.Id==2)
        }
        if (spec.orderBy != null)
        {
            query = query.OrderBy(spec.orderBy);
        }
        if (spec.orderByDescinding != null)
        {
            query = query.OrderByDescending(spec.orderByDescinding);
        }

        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }


        return query;
    }


    public static IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> Query, ISpecification<T, TResult> spec)
    {
        var query = Query;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);  //(x=?x=>x.Id==2)
        }
        if (spec.orderBy != null)
        {
            query = query.OrderBy(spec.orderBy);
        }
        if (spec.orderByDescinding != null)
        {
            query = query.OrderByDescending(spec.orderByDescinding);
        }

        var selectquery = query as IQueryable<TResult>;
        if (spec.Select != null)
        {
            selectquery = query.Select(spec.Select);
        }

        if (spec.IsDistinct)
        {
            selectquery = selectquery?.Distinct();
        }

        return selectquery ?? query.Cast<TResult>();
    }
}
