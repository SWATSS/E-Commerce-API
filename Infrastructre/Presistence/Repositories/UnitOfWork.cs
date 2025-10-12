using Domain.Contracts;
using Domain.Models;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        #region Create Reposotry (Like Lazy Way)
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            }
            // Create Repo Object
            var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            // Store Reference From Repo Object
            _repositories[typeName] = repo;
            return repo;
        } 
        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
