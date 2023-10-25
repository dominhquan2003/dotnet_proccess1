using Services.Helpers;
using Services.Models.Customers;
using Services.Repository;

namespace Services.Services
{
	public class CustomerService
	{
		private CustomerRepository _repository;
		private CartRepository _cartRepository;

		public CustomerService(MyDbContext db)
		{
			_repository = new CustomerRepository(db);
			_cartRepository = new CartRepository(db);
		}

		public IEnumerable<Customer> GetAllCustomers()
		{
			return _repository.GetAllCustomers();
		}

		public Customer GetCustomerByPhone(string phone)
		{
			return _repository.GetCustomerByPhone(phone);
		}

		// Create new customer and cart
		public void AddCustomer(Customer customer)
		{
			customer.Password = Generate.GenerateHashedPassword(customer.Password);
			_repository.AddCustomer(customer);
			_cartRepository.Create(customer);
		}
		// Update customer information
		public void UpdateCustomer(Customer customer)
		{
			_repository.UpdateCustomer(customer);
		}
		// Delete customer
		public void DeleteCustomer(int customerId)
		{
			_repository.DeleteCustomer(customerId);
		}
	}
}
