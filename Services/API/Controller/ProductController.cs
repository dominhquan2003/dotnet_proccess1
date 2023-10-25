using Microsoft.AspNetCore.Mvc;
using Services.API.RequestEntity;
using Services.Repository;
using Services.Services;

namespace Services.API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private ProductService _productService;

		public ProductController(MyDbContext db)
		{
			_productService = new ProductService(db);
		}

		// GET
		[HttpGet]
		public ActionResult GetProducts()
		{
			var products = _productService.GetAll();
			return Ok(products);
		}

		// GET
		[HttpGet("page")]
		public IActionResult GetProductsByPage(int pageNumber, int pageSize)
		{
			try
			{
				var products = _productService.GetProductsByPage(pageNumber, pageSize);
				return Ok(products);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while fetching the products.");
			}
		}

		// GET
		[HttpGet("filter")]
		public IActionResult FilterProducts([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
											[FromQuery] int? minStockQuantity, [FromQuery] int? maxStockQuantity,
											[FromQuery] int? categoryId)
		{
			try
			{
				var products = _productService.FilterProducts(minPrice, maxPrice,
															  minStockQuantity,
															  maxStockQuantity,
															  categoryId);

				//var response = new ApiResponse<IEnumerable<Product>>
				//{
				//    Success = true,
				//    Data = products,
				//    ErrorMessage = null
				//};

				return Ok(products);
			}
			catch (Exception ex)
			{
				//var response = new ApiResponse<IEnumerable<Product>>
				//{
				//    Success = false,
				//    Data = null,
				//    ErrorMessage = "An error occurred while filtering products."
				//};

				return StatusCode(500);
			}
		}
		// POST
		[HttpPost]
		public ActionResult CreateProduct([FromBody] ProductRequest product, List<IFormFile>? images)
		{
			if (product == null)
			{
				return BadRequest("Invalid product data");
			}
			try
			{

				_productService.CreateProduct(product.Name,
							product.Description,
							product.Price,
							product.StockQuantity,
							product.CategoryId);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while creating the product.");
			}
		}

		// PUT
		[HttpPut("{id}")]
		public IActionResult UpdateProduct(int id, [FromBody] ProductRequest product)
		{

			_productService.UpdateProduct(id, product.Name,
							product.Description,
							product.Price,
							product.StockQuantity,
							product.CategoryId);
			return Ok();
		}

		// DELETE
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			_productService.DeleteProduct(id);
			return Ok();
		}

	}
}
