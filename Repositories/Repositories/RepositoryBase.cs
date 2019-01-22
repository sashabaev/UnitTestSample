using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected abstract DbSet<TEntity> DbSet { get; }

        public virtual IQueryable<TEntity> All => DbSet.AsNoTracking();
        //protected virtual IQueryable<TEntity> All => DbSet.AsNoTracking();

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var entity = DbSet.Add(item).Entity;
           // await DbContext?.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await All.ToListAsync();
            }

            return All.Where(predicate.Compile()).ToList();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = All.FirstOrDefault(x => x.Id == id);
            DbSet.Remove(entity);
            await DbContext?.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            if (DbContext.Entry(item).State == EntityState.Detached)
            {
                DbSet.Attach(item);
            }

            DbSet.Update(item);

            await DbContext?.SaveChangesAsync();
        }
    }
}
