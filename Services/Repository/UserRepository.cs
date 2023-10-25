using Microsoft.EntityFrameworkCore;
using Services.Models.User;

namespace Services.Repository
{
	public class UserRepository
	{
		private readonly MyDbContext _db;

		public UserRepository(MyDbContext db)
		{
			_db = db;
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _db.Users.ToList();
		}

		public User GetUserByPhone(string phoneNumber)
		{
			var user = _db.Users.FirstOrDefault(u => u.PhoneNumber.Equals(phoneNumber));
			if (user != null)
			{
				return user;
			}
			throw new Exception("Not found");
		}

		public User? GetUserByEmail(string email)
		{
			var user = _db.Users.FirstOrDefault(u => u.Email.Equals(email));
			if (user != null)
			{
				return user;
			}
			return null;
		}

		public void UpdateUserPassword(int userId, string newPassword)
		{
			var user = _db.Users.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				user.Password = newPassword;
				_db.Entry(user).State = EntityState.Modified;
				_db.SaveChanges();
			}
		}

		public void UpdateUserRole(int userId, int roleId)
		{
			var user = _db.Users.FirstOrDefault(u => u.Id == userId);
			var newRole = _db.Roles.Find(roleId);
			if (user != null && newRole != null)
			{
				user.Role = newRole;
				_db.Entry(user).State = EntityState.Modified;
				_db.SaveChanges();
			}
		}

		public void AddUser(User user, int roleId)
		{
			try
			{
				var role = _db.Roles.FirstOrDefault(r => r.Id == roleId);
				if (role != null)
				{
					user.Role = role;
					_db.Users.Add(user);
					_db.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in AddUser:");
				Console.WriteLine($"Message: {ex.Message}");
				Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
				throw;  // Rethrow the exception to preserve the original error details
			}
		}

		public void UpdateUser(User user)
		{
			_db.Entry(user).State = EntityState.Modified;
			_db.SaveChanges();
		}

		public void DeleteUser(string phoneNumber)
		{
			var user = this.GetUserByPhone(phoneNumber);
			if (user != null)
			{
				_db.Users.Remove(user);
				_db.SaveChanges();
			}
		}
	}
}
