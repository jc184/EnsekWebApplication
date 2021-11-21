using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected EnsekDbContext EnsekDbContext { get; set; }

        public RepositoryBase(EnsekDbContext ensekContext)
        {
            this.EnsekDbContext = ensekContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges
                ? EnsekDbContext.Set<T>()
                    .AsNoTracking()
                : EnsekDbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges
                ? EnsekDbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : EnsekDbContext.Set<T>()
                    .Where(expression);

        public void Create(T entity)
        {
            this.EnsekDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.EnsekDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.EnsekDbContext.Set<T>().Remove(entity);
        }
    }
}
