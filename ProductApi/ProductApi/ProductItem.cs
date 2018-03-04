using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi
{
    public class ProductItem
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int ShopId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
    }
    public class ProductContext : DbContext
    {

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<ProductItem> ProductItems { get; set; }
    }

}
