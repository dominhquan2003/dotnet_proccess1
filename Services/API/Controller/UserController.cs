using Microsoft.AspNetCore.Mvc;
using Services.API.RequestEntities;
using Services.Helpers;
using Services.Models.User;
using Services.Repository;
using Services.Services;
using System.Globalization;

namespace Services.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(MyDbContext db)
		{
			_userService = new UserService(db);
		}

		[HttpGet]
		public IActionResult GetAllUsers()
		{
			try
			{
				var users = _userService.GetAllUsers();
				return Ok(users);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("{phoneNumber}")]
		public IActionResult GetUserByPhone(string phoneNumber)
		{
			try
			{
				var user = _userService.GetUserByPhone(phoneNumber);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("email/{email}")]
		public IActionResult GetUserByEmail(string email)
		{
			try
			{
				var user = _userService.GetUserByEmail(email);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost]
		public IActionResult AddUser([FromBody] UserRequest userRequest)
		{

			if (userRequest == null)
				return BadRequest("User request object is null");

			// Check if password and confirm password match
			if (userRequest.Password != userRequest.ConfirmPassword)
				return BadRequest("Password and ConfirmPassword do not match");

			var user = new User
			{
				Id = userRequest.Id,
				Name = userRequest.FullName,
				Email = userRequest.Email,
				PhoneNumber = userRequest.Phone,
				DateOfBirth = DateTime.ParseExact(userRequest.Dob, "dd/MM/yyyy", CultureInfo.InvariantCulture),
				Password = Generate.GenerateHashedPassword(userRequest.Password),
			};

			try
			{
				if (user == null)
					return BadRequest("User object is null");

				_userService.AddUser(user, userRequest.RoleId);

				return Ok("User added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.StackTrace}");
			}
		}

		[HttpPut("{userId}/update-password")]
		public IActionResult UpdateUserPassword(int userId, [FromBody] string newPassword)
		{
			try
			{
				_userService.UpdateUserPassword(userId, Generate.GenerateHashedPassword(newPassword));
				return Ok("User password updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{userId}/update-role/{roldeId}")]
		public IActionResult UpdateUserRole(int userId, int roleId)
		{
			try
			{
				_userService.UpdateUserRole(userId, roleId);
				return Ok("User role updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut]
		public IActionResult UpdateUser([FromBody] User user)
		{
			try
			{
				if (user == null)
					return BadRequest("User object is null");

				_userService.UpdateUser(user);
				return Ok("User updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{phoneNumber}")]
		public IActionResult DeleteUser(string phoneNumber)
		{
			try
			{
				_userService.DeleteUser(phoneNumber);
				return Ok("User deleted successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
