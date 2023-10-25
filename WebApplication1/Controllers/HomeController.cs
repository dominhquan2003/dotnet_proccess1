using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Services.Services;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private ProductService _productService;
		private CategoryService _categoryService;
		private int pageSize = 10;
		public HomeController(MyDbContext db)
		{
			_productService = new ProductService(db);
            _categoryService = new CategoryService(db);
        }

		public IActionResult Index()
		{
			var data = _productService.GetAll();

			ViewBag.Products = data;
			ViewBag.Categories = _categoryService.GetCategories();
			ViewBag.Page = Math.Floor((decimal)(data.Count / pageSize));
			return View();
		}

		[HttpGet("filter")]
		public IActionResult Filter(int? category, string sort, int minPrice, int maxPrice, int page)
		{
			if (maxPrice == 0 )
			{
				maxPrice = int.MaxValue;
			}

			if (string.IsNullOrEmpty(sort))
			{
				sort = "asc";
			}

            var filteredProducts = _productService.FilterProducts(minPrice, maxPrice, null, null, category);

            if (sort == "desc")
            {
                filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
            }
            else if (sort == "asc")
            {
                filteredProducts = filteredProducts.OrderBy(p => p.Price);
            }
			
            // Phân trang sản phẩm
            int pageSize = 12;
            int skipCount = (page - 1) * pageSize;
            var paginatedProducts = filteredProducts.Skip(skipCount).Take(pageSize).ToList();

            // Đặt dữ liệu cho ViewBag
            ViewBag.Products = paginatedProducts;
            ViewBag.Categories = _categoryService.GetCategories();
            ViewBag.Page = filteredProducts.Count();

            return PartialView("_FilteredProductList", paginatedProducts);
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}