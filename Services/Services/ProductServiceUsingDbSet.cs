using Repositories.Interfaces;
using Repositories.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductServiceUsingDbSet : IProductService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductServiceUsingDbSet(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productsRepository.All.ToList();
        }

        public ICollection<Product> GetTop5Products()
        {
            return _productsRepository.All.OrderByDescending(x => x.Price).Take(5).ToList();
        }

        public async Task AddProduct(Product product)
        {
            await _productsRepository.CreateAsync(product);
        }
    }
}
