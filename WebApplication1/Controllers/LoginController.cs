using Microsoft.AspNetCore.Mvc;
using Services.Helpers;
using Services.Models.Customers;
using Services.Repository;
using Services.Services;
using System.Text.Json;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
	[Route("/login")]
	public class LoginController : Controller
	{
		private CustomerService _customerService;
		private CartService _cartService;

		public LoginController(MyDbContext db)
		{
			_customerService = new CustomerService(db);
			_cartService = new CartService(db);
		}

		[HttpGet]
		public IActionResult Index()
		{
            var x = HttpContext.Session.GetString("CustomerId");

            if (HttpContext.Session.GetString("CustomerId") != null)
			{
				ViewData["Name"] = HttpContext.Session.Get("Name");
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var customer = _customerService.GetCustomerByPhone(username);

                if (customer == null)
                {
                    ViewBag.Message = "Cannot find your account";
                    return View();
                }
                else
                {
                    if (username.Equals(customer.Phone) && Generate.GenerateHashedPassword(password).Equals(customer.Password))
                    {
                        var cart = _cartService.GetCartByPhone(customer.Phone);

                        ViewData["username"] = username;

                        // save cart id 
                        HttpContext.Session.SetString("cartId", cart.Id.ToString());
                        HttpContext.Session.SetString("customerPhone", customer.Phone);
                        HttpContext.Session.SetString("Name", customer.Name);
                        HttpContext.Session.SetString("CustomerId", customer.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Username or password incorrect";
                        ViewBag.username = username;
                        ViewBag.password = password;
                        return View();
                    }
                }
            }

            return View();
        }


        [HttpGet("logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Session.Get("CustomerId") != null)
            {
                HttpContext.Session.Clear();
                return Redirect("/");
            }
            return RedirectToAction("Index", "Home");
        }


		[HttpGet("register")]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost("register")]
        public IActionResult Register(string phonenumber, string email, string password, string confirmpassword, string name, string address)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(phonenumber) && !string.IsNullOrEmpty(email))
            {
                var existingCustomer = _customerService.GetCustomerByPhone(phonenumber);
                
				if (existingCustomer != null)
                {
                    ViewBag.Message = "Username already exists. Please choose a different username.";
                    return View();
                }

				if (!password.Equals(confirmpassword))
				{
					ViewBag.Message = "Wrong confirm password!";
					ViewBag.Phone = phonenumber;
					ViewBag.Email = email;
					ViewBag.Password = password;
					ViewBag.Address = address;
				}

                var newCustomer = new Customer
                {
                    Phone = phonenumber,
					Email = email,
                    Password = password,
                    Name = name,
					Address = address
                };

                _customerService.AddCustomer(newCustomer);
				ViewData["Message"] = "Create success! Please login your account!";
                return RedirectToAction("Index", "Login");
			}
			ViewBag.Message = "Check your information!";
            return View();
        }

       
	}
}
