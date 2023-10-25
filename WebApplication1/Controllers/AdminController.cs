using Microsoft.AspNetCore.Mvc;
using Services.Helpers;
using Services.Models.User;
using Services.Repository;
using Services.Services;
using System.Text.Json;

namespace WebApplication1.Controllers
{
	[Route("/Admin")]
	public class AdminController : Controller
	{
		private ProductService _productService;
		private CustomerService _customerService;	
		private OrderService _orderService;	
		private CategoryService _categoryService;
		private UserService _userService;	

		public AdminController(MyDbContext db)
		{
			_productService = new ProductService(db);
			_customerService = new CustomerService(db);
			_orderService = new OrderService(db);
			_categoryService = new CategoryService(db);
			_userService = new UserService(db);
		}

		[HttpGet]
		public ActionResult Index()
		{
			var isLogin = HttpContext.Session.GetString("IsLogin");
			var isAdmin = HttpContext.Session.GetString("IsAdmin");

            if (string.IsNullOrEmpty(isLogin) && string.IsNullOrEmpty(isAdmin))
			{
				return Redirect("https://localhost:7222/admin/login");
			}

			ViewBag.Products = _productService.GetAll();
			return View();
		}

		[HttpGet("login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost("login")]
		public IActionResult Login(string username, string password)
		{
			var userPhone = _userService.GetUserByPhone(username);
			if (userPhone != null)
			{
				var hashPassword = Generate.GenerateHashedPassword(password);

				if( userPhone.Password.Equals(hashPassword))
				{
					HttpContext.Session.SetString("IsLogin", true.ToString());
					HttpContext.Session.SetString("IsAdmin", true.ToString());
					/* if (userEmail != null)
					{
						HttpContext.Session.SetString("UserInfo", JsonSerializer.Serialize<User>(userEmail));
					}
					if(userPhone != null)
					{
                        HttpContext.Session.SetString("UserInfo", JsonSerializer.Serialize<User>(userPhone));
                    } */
                    return RedirectToAction("Index");
				}
			}
            return RedirectToAction("Login");
		}


        [HttpGet("product/edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("product/edit/{id}")]
        public IActionResult Edit(int id, string name)
        {
            return View();
        }

        [HttpGet("product/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index","Admin");
        }
    }
}
