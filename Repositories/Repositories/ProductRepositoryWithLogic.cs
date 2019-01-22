using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.Repositories
{
    public class ProductRepositoryWithLogic : RepositoryBase<Product>, IProductsRepositoryWithLogic
    {
        public ICollection<Product> GetProducts()
        {
            return All.ToList();
        }

        public ICollection<Product> GetTop5Products()
        {
            //server side if sql
            //var y = All.Where(x => x.Name.Contains("f")).OrderBy(x => x.Id);
            return All.OrderByDescending(x => x.Price).Take(5).ToList();
        }

        public IQueryable<Product> GetTop5ProductsAsExpression()
        {
            return All.OrderByDescending(x => x.Price).Take(5);
        }

        public ProductRepositoryWithLogic(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Product> DbSet => DbContext.Products;
    }
}
