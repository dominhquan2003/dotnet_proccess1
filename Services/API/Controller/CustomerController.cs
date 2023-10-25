using Microsoft.AspNetCore.Mvc;
using Services.API.RequestEntities;
using Services.Helpers;
using Services.Models.Customers;
using Services.Repository;
using Services.Services;

namespace Services.API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly CustomerService _customerService;

		public CustomerController(MyDbContext db)
		{
			_customerService = new CustomerService(db);
		}

		[HttpGet]
		public IActionResult GetAllCustomers()
		{
			try
			{
				var customers = _customerService.GetAllCustomers();
				return Ok(customers);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetCustomerById(int id)
		{
			try
			{
				//var customer = _customerService.GetCustomerById(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost]
		public IActionResult AddCustomer([FromBody] CustomerRequest customer)
		{
			if (!customer.Password.Equals(customer.ConfirmPassword))
			{
				return BadRequest("Confirm passowrd doesn't match password");
			}

			try
			{
				if (customer == null)
					return BadRequest("Customer object is null");

				var newCus = new Customer()
				{
					Name = customer.FullName,
					Address = customer.Address,
					Email = customer.Email,
					Phone = customer.Phone,
					Password = Generate.GenerateHashedPassword(customer.Password)
				};

				_customerService.AddCustomer(newCus);
				return Ok("Customer added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{userId}/update-password")]
		public IActionResult UpdateUserPassword(int userId, [FromBody] PasswordUpdateRequest passwordUpdateRequest)
		{
			try
			{
				//var user = _customerService.GetCustomerById(userId);
				//if (user == null)
				//    return NotFound("User not found.");

				//string hashedOldPassword = Generated.GenerateHashedPassword(passwordUpdateRequest.OldPassword);

				//if (hashedOldPassword != user.Password)
				//    return BadRequest("Old password is incorrect.");

				//string hashedNewPassword = Generated.GenerateHashedPassword(passwordUpdateRequest.NewPassword);

				//user.Password = hashedNewPassword;
				//_customerService.UpdateCustomer(user);

				return Ok("User password updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCustomer(int id, [FromBody] CustomerRequest customer)
		{
			try
			{
				if (customer == null)
					return BadRequest("Customer object is null");

				if (id != customer.Id)
					return BadRequest("Customer Id mismatch");

				var newCus = new Customer()
				{
					Name = customer.FullName,
					Address = customer.Address,
					Email = customer.Email,
					Phone = customer.Phone,
					Password = Generate.GenerateHashedPassword(customer.Password)
				};

				_customerService.UpdateCustomer(newCus);
				return Ok("Customer updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCustomer(int id)
		{
			try
			{
				_customerService.DeleteCustomer(id);
				return Ok("Customer deleted successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
