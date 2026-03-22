using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInventoryAPI.Data;
using ProductInventoryAPI.Models;
namespace ProductInventoryAPI.Controllers
{
    [Route("api/[controller]")]     //handles API requests(get,post,put,delete)
    [ApiController]
    public class ProductsController:ControllerBase
    {
        private readonly ProductDbContext _db;
        public ProductsController(ProductDbContext context)
        {
            _db = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _db.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return product;
        }
        [HttpPost]
        public async Task<IActionResult> AddProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                if (product.Price <= 0)
                    return BadRequest("Price should be greater than 0");
            }

            await _db.Products.AddRangeAsync(products);
            await _db.SaveChangesAsync();

            return Ok(products);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Id mismatch");

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch
            {
                if (!_db.Products.Any(p => p.Id == id))
                    return NotFound("Product not found");

                throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
        {
            return await _db.Products
                .Where(p => p.Category == category)
                .ToListAsync();
        }
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<Product>>> GetSorted()
        {
            return await _db.Products
                .OrderBy(p => p.Price)
                .ToListAsync();
        }
        [HttpGet("outofstock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetOutOfStock()
        {
            return await _db.Products
                .Where(p => p.StockQuantity == 0)
                .ToListAsync();
        }
    }
}
