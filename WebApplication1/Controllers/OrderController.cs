using Microsoft.AspNetCore.Mvc;
using Services.Models.Cart;
using Services.Models.Customers;
using Services.Models.Order;
using Services.Repository;
using Services.Services;
using System;
using System.Text.Json;

namespace WebApplication1.Controllers
{
	[Route("/order")]
	public class OrderController : Controller
	{
		private string _orderId;
		private CartService _cartService;
		private OrderService _orderService;
		private CustomerService _customerService;
		public OrderController(MyDbContext db)
		{
            _orderService = new OrderService(db);
			_cartService = new CartService(db);
            _customerService = new CustomerService(db);
        }

		[HttpGet]
		public IActionResult AllOrder()
		{
			var cusId = HttpContext.Session.GetString("CustomerPhone");
			if (cusId != null)
				ViewBag.Orders = _orderService.GetAllOrder(cusId);
			return View("Index");
		}

		[HttpGet("payment")]
		public IActionResult GetPayment([FromQuery] decimal total, [FromQuery] int cartId)
		{
			var cusId = HttpContext.Session.GetString("CustomerId");

			ViewBag.Total = total;
			ViewBag.CartId = cartId;	
			ViewBag.CustomerId = cusId;
			ViewBag.ListProduct = _cartService.GetCartDetails(cartId);

			return View();
		}

		[HttpPost("payment")]
		public IActionResult SubmitPayment([FromForm]int cartId,[FromForm]decimal total, [FromForm]string info, [FromForm]string bankcode="VNPAYQR")
		{
			var customerPhone = HttpContext.Session.GetString("customerID");

            if (customerPhone == null)
			{
				return RedirectToAction("Failed");
			}

			var orderRefId = Guid.NewGuid().ToString().Replace("-","");

			Order order = new Order() { 
				ReferenceId = orderRefId,
				Status = false,
				TotalAmount = total,
				OrderDate = DateTime.Now,
				Customer = _customerService.GetCustomerByPhone(customerPhone)
            };
			
			var vnp_url = VNPayService.GeneratePaymentUrl(total, bankcode, info, orderRefId);

			_orderService.CreateOrder(order);
			return Redirect(vnp_url);
		}

		[HttpGet("callback")]
		public IActionResult CallBack()
		{
			var Request = HttpContext.Request;

            string? vnpResponseCode = Request.Query["vnp_ResponseCode"];
            string? vnp_TxnRef = Request.Query["vnp_TxnRef"];

            if (vnpResponseCode == "00" && !string.IsNullOrEmpty(vnp_TxnRef))
            {

				_orderService.UpdateOrder(vnp_TxnRef);
				TempData["Message"] = $"Order success";
				TempData["Date"] = DateTime.Now.ToString();
                TempData["orderID"] = vnp_TxnRef;
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

		[HttpGet("success")]
		public IActionResult Succes() {
			return View(); 
		}
		
		[HttpGet("failed")]
        public IActionResult Failed() {
            return View(); 
		}
	}
}
