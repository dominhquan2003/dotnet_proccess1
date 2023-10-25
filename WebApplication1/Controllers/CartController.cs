using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Models.Cart;
using Services.Repository;
using Services.Services;
using System;
using System.Configuration;

public class ModelProduct
{
	public int cartId { get; set; }
	public int productId { get; set; }
	public int quantity { get; set; }
}
public class CartUpdate
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

namespace WebApplication1.Controllers
{


	[Route("/cart")]
	public class CartController : Controller
	{
		private CartService _cartService;
		private CustomerService _cutomerService;

		public CartController(MyDbContext db)
		{
			_cartService = new CartService(db);
			_cutomerService = new CustomerService(db);
		}

		[HttpGet]
		public IActionResult Index()
		{
			var cusID = HttpContext.Session.GetString("customerID");
			var cartId = HttpContext.Session.GetString("cartId");



			if (cusID == null && cartId == null)
			{
				ViewBag.Message = "Please login to use cart feature!";
				return View();
			}
			else
			{
				int.TryParse(cartId, out int cartIdInt);
				var cart = _cartService.GetCartById(cartIdInt);
				var cartDetails = _cartService.GetCartDetails(cartIdInt);

				decimal sum = cartDetails.Sum((cd) => cd.Product.Price * cd.Quantity);

				ViewBag.ListProduct = cartDetails;
				ViewBag.CartId = cart.Id;
				ViewBag.Total = sum;
				return View();

			}

		}

		[HttpGet("{id}")]
		public IActionResult Index(int id)
		{
			var cusID = HttpContext.Session.GetString("customerID");
			if (cusID == null)
			{
				ViewBag.Message = "Please login to use cart feature!";
				return View();
			}
			else
			{
				ViewData["username"] = HttpContext.Session.GetString("username");
				ViewData["Name"] = HttpContext.Session.GetString("Name");

				var cart = _cartService.GetCartById(id);
				var cartDetails = _cartService.GetCartDetails(id);

				decimal sum = cartDetails.Sum((cd) => cd.Product.Price * cd.Quantity);

				ViewBag.ListProduct = cartDetails;
				ViewBag.Cart = cart;
				ViewBag.Total = sum;
				return View();
			}
		}

		[HttpPost]
		public IActionResult Index([FromForm] ModelProduct model)
		{
			var cusID = HttpContext.Session.GetString("customerID");

			if (model == null) return BadRequest("null");

			if (cusID == null)
			{
				ViewBag.Message = "Please login to use cart feature!";
				return View();
			}
			else
			{
				var cart = _cartService.GetCartById(model.cartId);

				var cartDetails = _cartService.GetCartDetails(model.cartId);

				var existing = cartDetails.FirstOrDefault(c => c.Product.Id == model.productId);

				if (existing != null)
				{
					_cartService.UpdateCartItem(model.cartId, model.productId, model.quantity + existing.Quantity);
				}
				else
				{
					_cartService.AddToCart(model.cartId, model.productId);
				}

				cartDetails = _cartService.GetCartDetails(model.cartId);

				decimal sum = cartDetails.Sum((cd) => cd.Product.Price * cd.Quantity);

				ViewBag.ListProduct = cartDetails;
				ViewBag.Cart = cart;
				ViewBag.Total = sum;

				return RedirectToAction("Index");
			}
		}

		[HttpGet("delete")]
		public IActionResult Delete([FromQuery] int cartId, [FromQuery] int productId)
		{
			_cartService.RemoveFromCart(cartId, productId);
			return RedirectToAction("Index");
		}

		[HttpPut("update")]
		public IActionResult UpdateCart([FromForm] CartUpdate cartUpdate)
		{
			try
			{
				_cartService.UpdateCartItem(cartUpdate.CartId, cartUpdate.ProductId, cartUpdate.Quantity);
				var response = new
				{
					Code = 0,
					Message = "Cart item updated successfully"
				};
				return new JsonResult(response);
			}
			catch (Exception ex)
			{
				var response = new
				{
					Code = 1,
					Message = $"Failed to update the cart item. Detail: {ex.Message}"
				};
				return new JsonResult(response);
			}
		}
	}
}
