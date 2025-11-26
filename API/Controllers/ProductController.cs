using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

	public class ProductController(IGenericRepository<Product> PR) : BaseAPIController
	{


		// GET: api/product
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
		{
			return Ok(await PR.ListAllAsync());
		}


		// GET: api/product
		// GET: api/product
		[HttpGet("Filter")]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var spec = new ProductSpecification(specParams);
			return await CreatePageResult(PR, spec, specParams.PageIndex, specParams.pagesize);
		}

		// 	[HttpGet("Filter")]
		// 	public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] string? brands = null,
		// [FromQuery] string? types = null,
		// [FromQuery] string? sort = null)
		// 	{


		// 		var specParams = new ProductSpecParams
		// 		{
		// 			// If null or empty, create empty list
		// 			Brands = string.IsNullOrWhiteSpace(brands)
		// 			? new List<string>()
		// 			: brands.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),

		// 			Types = string.IsNullOrWhiteSpace(types)
		// 			? new List<string>()
		// 			: types.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),

		// 			Sort = sort // Can be null
		// 		};


		// 		var spec = new ProductSpecification(specParams);
		// 		var products = await PR.GetListWithSpecAsync(spec);

		// 		return Ok(products);
		// 	}



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
