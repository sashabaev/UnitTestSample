using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ProductRepositoryNoDbContextControl : IRepository<Product>
    {
        public IQueryable<Product> All => throw new NotImplementedException();

        public async Task<Product> CreateAsync(Product item)
        {
            using (var db = new ApplicationDbContext())
            {
                await db.Products.AddAsync(item);
                db.SaveChanges();
            }
            return item;
        }

        public async Task<IList<Product>> FindAsync(Expression<Func<Product, bool>> predicate = null)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Products.Where(predicate.Compile()).ToList();
            }
        }

        public async Task RemoveAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity = db.Products.FirstOrDefault(x => x.Id == id);
                db.Products.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product item)
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Entry(item).State == EntityState.Detached)
                {
                    db.Products.Attach(item);
                }

                db.Products.Update(item);
                await db.SaveChangesAsync();
            }
        }
    }
}
