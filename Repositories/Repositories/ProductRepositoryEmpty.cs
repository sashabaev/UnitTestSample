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
    public class ProductRepositoryEmpty : RepositoryBase<Product>, IProductsRepository
    {
        public ProductRepositoryEmpty(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Product> DbSet => DbContext.Products;
    }
}
