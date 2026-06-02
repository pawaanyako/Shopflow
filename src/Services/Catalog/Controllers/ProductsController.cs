using Catalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = [
            new Product { Id = 1, Name = "Green Apple", Category = "Fruits", Description = "Green colored Apple", Price = 20.0M, Stock = 10},
            new Product { Id = 2, Name = "Toy car", Category = "Toys", Description = "Toy Mazda RX-7 model", Price = 100.0M, Stock = 20},
            new Product { Id = 3, Name = "Ascorbic acid", Category = "Medicine", Description = "Round yellow tablets", Price = 5.0M, Stock = 30}
            ];

        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product product)
        {
            product.Id = _products.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => Ok(_products);

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product is null) 
                return NotFound();
            
            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, [FromBody] Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound();
            
            product.Name = updatedProduct.Name;
            product.Category = updatedProduct.Category;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    }
}
