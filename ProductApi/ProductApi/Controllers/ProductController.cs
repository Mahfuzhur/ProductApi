using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private ProductContext _productContext;

        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
            if (productContext.ProductItems.Count() == 0)
            {
                _productContext.ProductItems.Add(new ProductItem { ShopId = 1, ProductName = "Cola", Price = 18, Quantity = 100 });
                _productContext.ProductItems.Add(new ProductItem { ShopId = 2, ProductName = "Cola", Price = 16, Quantity = 10 });
                _productContext.ProductItems.Add(new ProductItem { ShopId = 3, ProductName = "Cola", Price = 20, Quantity = 120 });

                _productContext.SaveChanges();

            }
        }
        [HttpGet]
        public IEnumerable<ProductItem> GetAll()
        {
            return _productContext.ProductItems.AsNoTracking().ToList();
        }
        [HttpGet("{id}/{shopId}")]
        public IActionResult GetById(long id, int shopId)
        {
            var product = _productContext.ProductItems.FirstOrDefault(p => p.Id==id && p.ShopId == shopId);
            if(product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
    }
}