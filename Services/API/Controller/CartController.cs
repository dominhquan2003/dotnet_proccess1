using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Services.Services;

namespace Services.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CartController : ControllerBase
	{
		private readonly CartService _cartService;

		public CartController(MyDbContext db)
		{
			_cartService = new CartService(db);
		}

		[HttpGet("{cartId}/details")]
		public IActionResult GetCartDetails(int cartId)
		{
			try
			{
				var cartDetails = _cartService.GetCartDetails(cartId);
				return Ok(cartDetails);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost("{cartId}")]
		public IActionResult AddToCart(int cartId, [FromBody] int productId)
		{
			try
			{
				_cartService.AddToCart(cartId, productId);
				return Ok("Item added to cart successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{cartDetailId}")]
		public IActionResult RemoveFromCart(int cartId, int cartDetailId)
		{
			try
			{
				_cartService.RemoveFromCart(cartId, cartDetailId);
				return Ok("Item removed from cart successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
