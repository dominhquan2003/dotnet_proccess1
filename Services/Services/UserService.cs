using Services.Models.User;
using Services.Repository;

namespace Services.Services
{
	public class UserService
	{
		private readonly UserRepository _userRepository;

		public UserService(MyDbContext db)
		{
			_userRepository = new UserRepository(db);
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAllUsers();
		}

		public User GetUserByPhone(string phoneNumber)
		{
			return _userRepository.GetUserByPhone(phoneNumber);
		}

		public User GetUserByEmail(string email)
		{
			return _userRepository.GetUserByEmail(email);
		}

		public void UpdateUserPassword(int userId, string newPassword)
		{
			_userRepository.UpdateUserPassword(userId, newPassword);
		}

		public void UpdateUserRole(int userId, int roleId)
		{
			_userRepository.UpdateUserRole(userId, roleId);
		}

		public void AddUser(User user, int roleId)
		{
			_userRepository.AddUser(user, roleId);
		}

		public void UpdateUser(User user)
		{
			_userRepository.UpdateUser(user);
		}

		public void DeleteUser(string phoneNumber)
		{
			_userRepository.DeleteUser(phoneNumber);
		}
	}
}
