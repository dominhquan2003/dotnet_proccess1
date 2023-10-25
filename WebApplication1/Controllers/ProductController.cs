using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Services.Services;

namespace WebApplication1.Controllers
{
	[Route("/Product")]
	public class ProductController : Controller
	{
		private ProductService _productService;
		public ProductController(MyDbContext db)
		{
			_productService = new ProductService(db);
		}

		[HttpGet("{id}")]
		public IActionResult Detail(int id)
		{
			if (HttpContext.Session.Get("customerID") != null)
			{
				ViewData["Name"] = HttpContext.Session.GetString("Name");
			}

			string? cartId = HttpContext.Session.GetString("cartId");

			var result = _productService.GetById(id);

			ViewBag.ProductDetail = result;
			ViewBag.CartId = cartId;

			if (result != null)
			{
				return View();

			}
			else return View("Error");
		}

		[HttpGet("filter")]
		public IActionResult FilterProducts([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
											[FromQuery] int? minStockQuantity, [FromQuery] int? maxStockQuantity,
											[FromQuery] int? categoryId)
		{
			var products = _productService.FilterProducts(minPrice, maxPrice, minStockQuantity, maxStockQuantity, categoryId);
			ViewBag.Products = products;
			return View();
		}
    }
}
