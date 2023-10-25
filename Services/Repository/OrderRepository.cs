using Services.Models.Cart;
using Services.Models.Order;

namespace Services.Repository
{
    public class OrderRepository
    {
        private MyDbContext _db;
        public OrderRepository(MyDbContext db) {
            _db =  db;
        }

        public void CreateOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public Order GetByReferenceId(string refId)
        {
            return _db.Orders.FirstOrDefault(o => o.ReferenceId.Equals(refId));
        }

        public List<Order> GetAllOrderByCustomrerPhone(string phone)
        {
            var customer = _db.Customers.First(c => c.Phone.Equals(phone));

            var res = from order in _db.Orders where order.Customer.Phone.Equals(customer.Phone) select order;

            return res.ToList();
        }

        public void UpdateOrderDetails(Order order,List<CartDetail> cartDetails)
        {
            foreach(var c in cartDetails)
            {
                decimal totalPrice = c.Product.Price * c.Quantity;
                var orderDetail = new OrderDetail() { Order = order , Product = c.Product ,Quantity = c.Quantity, TotalPrice = totalPrice };
                _db.OrderDetails.Add(orderDetail);
            }
            _db.SaveChanges();
        }

        public void UpdateStatus(string refId)
        {
            var res = _db.Orders.FirstOrDefault(o => o.ReferenceId.Equals(refId));
            if(res != null)
            {
                res.Status = true;
            }
            _db.SaveChanges();
        }

    }
}
