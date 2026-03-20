using Microsoft.EntityFrameworkCore;
using ProductInventoryAPI.Models;
namespace ProductInventoryAPI.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            :base(options)
        {
        }
        public DbSet<Product> Products{get;set;}
    }
}
