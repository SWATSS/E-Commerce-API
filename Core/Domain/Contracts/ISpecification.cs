using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; } // For Where Condition
        List<Expression<Func<TEntity, object>>> IncludeExpressions {  get; } // For Include Conditions
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginated { get; set; }
    }
}
