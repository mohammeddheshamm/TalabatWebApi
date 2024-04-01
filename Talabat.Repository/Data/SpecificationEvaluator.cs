using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery ,ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            //_context.Set<Product>()

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            //_context.Set<Product>().Where(P => P.Id == 10)

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            query = spec.Includes.Aggregate(query , (currentQuery , includeExpression) => currentQuery.Include(includeExpression));

            //_context.Set<Product>().Where(P => P.Id == 10).Include(P => P.ProductBrand).Include(P => P.ProductType);

            return query;
        }
    }
}
