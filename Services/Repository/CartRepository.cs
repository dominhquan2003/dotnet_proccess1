using Microsoft.AspNetCore.Components;
using Services.Models.Cart;
using Services.Models.Customers;
using System.Linq;
using System.Runtime.InteropServices;

namespace Services.Repository
{
	
	public class CartRepository
	{
		private readonly MyDbContext _db;
		private readonly ProductRepository _productRepository;
		public CartRepository(MyDbContext db)
		{
			_db = db;
			_productRepository = new ProductRepository(db);
		}

		// GET CART BY CUSTOMER
		public Cart? GetCartByCustomerId(Customer customer)
		{
			return _db.Carts.Where(c => c.Customer.Id == customer.Id).First();
		}

		public Cart? GetById(int Id)
		{
			return _db.Carts.FirstOrDefault(c => c.Id == Id);
		}

		// CREATE THE FIRST CART
		public void Create(Customer c)
		{
			_db.Carts.Add(new Cart() { Customer = c, CreatedAt = DateTime.Now});
			_db.SaveChanges();
		}

		// UPDATE ONE PRODUCT IN CART
		public void UpdateItem(int cartId, int productId, int quan)
		{
			var item = _db.CartDetails.FirstOrDefault(cd => cd.Product.Id == productId && cd.CartId == cartId);
			if (item != null)
			{
				item.Quantity = quan;
				_db.SaveChanges();
			}
		}

		// GET ALL PRODUCTS EXIST IN CART
		public List<CartDetail> GetCartDetails(int cartId)
		{
			return _db.CartDetails.Where(c => c.CartId == cartId).ToList();
		}
		// ADD ONE PRODUCT TO CART
		public void AddToCart(int cartId, int productId)
		{
			var product = _productRepository.GetProductById(productId);
			if (product != null)
			{
				var detail = new CartDetail() { CartId = cartId, Product = product, Quantity = 1 };
				_db.CartDetails.Add(detail);
				_db.SaveChanges();
			}
		}

		// DELETE ALL
		public void RemoveAllItem(int cartId)
		{
			var items = GetCartDetails(cartId);
			foreach (var item in items)
			{
				_db.CartDetails.Remove(item);
			}
			_db.SaveChanges() ;
		}

		// DELETE ONE
		public void RemoveFromCart(int cartId, int productId)
		{
			var cartDetail = _db.CartDetails.FirstOrDefault(cd => cd.CartId == cartId && cd.Product.Id == productId);
			if (cartDetail != null)
			{
				_db.CartDetails.Remove(cartDetail);
				_db.SaveChanges();
			}
		}
	}
}
