using Services.Models.Cart;
using Services.Repository;

namespace Services.Services
{
	public class CartService
	{
		private readonly CartRepository _cartRepository;
		private readonly CustomerService _customerService;

		public CartService(MyDbContext db)
		{
			_cartRepository = new CartRepository(db);
			_customerService = new CustomerService(db);
		}

		public Cart? GetCartById(int Id)
		{
			return _cartRepository.GetById(Id);
		}

		public Cart GetCartByPhone(string phone)
		{
			var customer = _customerService.GetCustomerByPhone(phone);
			
			if (customer != null)
			{
				var cart = _cartRepository.GetCartByCustomerId(customer);
				if (cart != null) return cart;
				else throw new Exception("Cannot find cart by phone number! Please check again!");
			}
			throw new Exception($"Cannot find username with phone number: {phone}");
		}

		public List<CartDetail> GetCartDetails(int cartId)
		{
			var cartDetails = _cartRepository.GetCartDetails(cartId);
			return cartDetails;
		}

		public void UpdateCartItem(int cartId, int productId, int quan)
		{
			_cartRepository.UpdateItem(cartId, productId, quan);
		}

		public void AddToCart(int cartId, int productId)
		{
			_cartRepository.AddToCart(cartId, productId);
		}

		public void RemoveFromCart(int cartId, int productId)
		{
			_cartRepository.RemoveFromCart(cartId, productId);
		}
	}
}
