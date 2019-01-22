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
    public class ProductServiceUsingRepositoryLogic : IProductService
    {
        private readonly IProductsRepositoryWithLogic _productsRepository;

        public ProductServiceUsingRepositoryLogic(IProductsRepositoryWithLogic productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productsRepository.GetProducts();
        }

        public ICollection<Product> GetTop5Products()
        {
            return _productsRepository.GetTop5Products();
        }

        public ICollection<Product> GetTop5ProductsSortedSql()
        {
            return _productsRepository.GetTop5ProductsAsExpression().OrderBy(x => x.Name).ToList();
        }

        public ICollection<Product> GetTop5ProductsSortedMemory()
        {
            return GetTop5Products().OrderBy(x => x.Name).ToList();
        }

        public async Task AddProduct(Product product)
        {
            await _productsRepository.CreateAsync(product);
        }
    }
}
