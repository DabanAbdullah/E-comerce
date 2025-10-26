using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IGenericRepository<Product> PR) : ControllerBase
    {


        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
        {
            return Ok(await PR.ListAllAsync());
        }


        // GET: api/product
        [HttpGet("Filter")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);
            var products = await PR.GetListWithSpecAsync(spec);

            return Ok(products);
        }



        // GET: api/product
        [HttpGet("GetTypes")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            var Types = await PR.GetListWithSpecAsync(spec); //to implement later
            return Ok(Types);

        }

        [HttpGet("GetBrands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {

            var spec = new BrandListSpecification();
            var Brands = await PR.GetListWithSpecAsync(spec); //to implement later
            return Ok(Brands);
        }



        // GET: api/product/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {


            return await PR.GetByIdAsync(id);
        }


        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            PR.AddAsync(product);
            if (await PR.SaveAllAsync())
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

            PR.UpdateAsync(product);
            if (!await PR.SaveAllAsync())
                return BadRequest("Failed to update product.");

            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await PR.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            PR.DeleteAsync(product);
            if (!await PR.SaveAllAsync())
                return BadRequest("Failed to Delete product.");

            return NoContent();


        }




        private bool ProductExists(int id)
        {
            return PR.Exists(id);
        }
    }
}
