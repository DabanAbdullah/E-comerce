using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductsRepository PR) : ControllerBase
    {


        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
        {
            return Ok(await PR.GetProductsAsync());
        }

        // GET: api/product/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {


            return await PR.GetProductByIdAsync(id);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            PR.CreateProduct(product);
            if (await PR.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }

            return BadRequest("Failed to create product.");
        }

        // PUT: api/product/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            PR.UpdateProduct(product);
            if (!await PR.SaveChangesAsync())
                return BadRequest("Failed to update product.");

            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await PR.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            PR.DeleteProduct(product);
            if (!await PR.SaveChangesAsync())
                return BadRequest("Failed to Delete product.");

            return NoContent();


        }
    }
}
