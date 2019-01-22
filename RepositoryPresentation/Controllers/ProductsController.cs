using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryPresentation.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITransactionManager _transactionManager;

        public ProductsController(IProductService productService, ITransactionManager transactionManager)
        {
            _productService = productService;
            _transactionManager = transactionManager;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        //[HttpGet]
        //[Route("getExpression")]
        //public IEnumerable<Product> GetExpression()
        //{
        //    return _productService.GetAllProductsExpression();
        //}

        [Route("getTop5")]
        [HttpGet]
        public void GetTop5()
        {
            _productService.GetTop5Products();
        }

        [HttpPost]
        public void Post([FromBody]Product product)
        {
            _productService.AddProduct(product);
        }

        [HttpGet]
        [Route("add")]
        public async Task AddDummyProducts()
        {
            //await _transactionManager.ExecuteInTransaction(
            //    async () =>
            //    {
                    await _productService.AddProduct(new Product { Id = 1, Name = "Coca Cola", Price = 2 });
                    await _productService.AddProduct(new Product { Id = 2, Name = "Fanta", Price = 2 });
                    await _productService.AddProduct(new Product { Id = 3, Name = "Snikers", Price = 3 });
              //  });
        }
    }
}
