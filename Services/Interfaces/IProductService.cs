using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        ICollection<Product> GetAllProducts();

        Task AddProduct(Product product);

        ICollection<Product> GetTop5Products();
    }
}
