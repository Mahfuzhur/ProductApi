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
        [HttpGet("{id}/{shopId}", Name = "GetProduct")]
        public IActionResult GetById(long id, int shopId)
        {
            var product = _productContext.ProductItems.FirstOrDefault(p => p.Id==id && p.ShopId == shopId);
            if(product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProductItem product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _productContext.ProductItems.Add(product);
            _productContext.SaveChanges();
            return CreatedAtRoute("GetProduct", new { 
                id = product.Id,
                shopId = product.ShopId
            },product);
        }
        [HttpPut("{id}/{shopId}")]
        public IActionResult Update(long id, int shopId, [FromBody] ProductItem product)
        {
            if(product == null || product.Id !=id || product.ShopId != shopId)
            {
                return BadRequest();
            }
            var pro = _productContext.ProductItems.FirstOrDefault(p => p.Id == id && p.ShopId == shopId);
            if(pro == null)
            {
                return NotFound();
            }
            pro.Price = product.Price;
            pro.ProductName = product.ProductName;
            pro.Quantity = product.Quantity;
            pro.Discount = product.Discount;
            _productContext.ProductItems.Update(pro);
            _productContext.SaveChanges();
            return new NoContentResult();
        }
    }
}