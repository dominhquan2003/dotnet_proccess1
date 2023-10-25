using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using Services.Models.Cart;
using Services.Models.Customers;
using Services.Models.Order;
using Services.Repository;

namespace Services.Services
{
	public class OrderService
	{
		private CartRepository cartRepository;
		private OrderRepository orderRepository;

		public OrderService(MyDbContext db)
		{
            orderRepository = new OrderRepository(db);
            cartRepository = new CartRepository(db);
        }

		public List<Order> GetAllOrder(string cusID)
		{
			return orderRepository.GetAllOrderByCustomrerPhone(cusID); ;
		}

		public void CreateOrder(Order order)
		{	
			orderRepository.CreateOrder(order);
		}

		public Order? GetOrder(string refId)
		{
			return orderRepository.GetByReferenceId(refId);
        }

		public void UpdateOrder(string refId)
		{
			var order = orderRepository.GetByReferenceId(refId);
			if (order != null)
			{
				var cart = cartRepository.GetCartByCustomerId(order.Customer);
				if (cart != null)
				{
                    var cartDetails = cartRepository.GetCartDetails(cart.Id);
                    orderRepository.UpdateOrderDetails(order, cartDetails);
                    foreach (var c in cartDetails)
                    {
                        cartRepository.RemoveFromCart(cart.Id, c.Product.Id);
                    }
                }
			}
			orderRepository.UpdateStatus(refId);
		}
	}
}
