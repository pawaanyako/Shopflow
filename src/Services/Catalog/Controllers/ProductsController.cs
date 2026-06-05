using Catalog.Data;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(CatalogDbContext context) : ControllerBase
    {
        private readonly CatalogDbContext _context = context;

        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.Category = updatedProduct.Category;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
