using Repositories.Models;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IProductsRepositoryWithLogic : IRepository<Product>
    {
        ICollection<Product> GetProducts();

        ICollection<Product> GetTop5Products();

        IQueryable<Product> GetTop5ProductsAsExpression();
    }
}
