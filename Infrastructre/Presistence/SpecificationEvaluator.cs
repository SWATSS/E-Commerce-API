using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    internal static class SpecificationEvaluator
    {
        // Create Query
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecification<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            // _dbContext.Products
            var query = InputQuery;
            // Where
            if (specification.Criteria is not null)
            {
                // _dbContext.Products.Where(P => P.Id == id)
                query = query.Where(specification.Criteria);
            }

            // OrderBy
            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPaginated)
            {
                query = query.Skip(specification.Skip);
                query = query.Take(specification.Take);
            }

            // Includes
            if (specification.IncludeExpressions is not null && specification.IncludeExpressions.Any())
            {
                /// With Loop
                ///foreach (var expression in specification.IncludeExpressions)
                ///{
                ///    query = query.Include(expression);
                ///}

                // Aggregate Operators

                // _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)
                query = specification.IncludeExpressions.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }

            return query;
        }
    }
}
