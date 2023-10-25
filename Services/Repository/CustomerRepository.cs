using Microsoft.EntityFrameworkCore;
using Services.Models.Customers;

namespace Services.Repository
{
	public class CustomerRepository
	{
		private readonly MyDbContext _db;

		public CustomerRepository(MyDbContext db)
		{
			_db = db;
		}

		public IEnumerable<Customer> GetAllCustomers()
		{
			return _db.Customers.ToList();
		}

		public Customer? GetCustomerById(int customerId)
		{
			return _db.Customers.FirstOrDefault(c => c.Id == customerId);
		}

		public Customer? GetCustomerByEmail(string email)
		{
			return _db.Customers.FirstOrDefault(c => c.Email == email);
		}

		public Customer GetCustomerByPhone(string phone)
		{
			return _db.Customers.FirstOrDefault(c => c.Phone.Equals(phone));
		}

		public void AddCustomer(Customer customer)
		{
			_db.Customers.Add(customer);
			_db.SaveChanges();
		}

		public void UpdateCustomer(Customer customer)
		{
			_db.Entry(customer).State = EntityState.Modified;
			_db.SaveChanges();
		}

		public void DeleteCustomer(int customerId)
		{
			var customer = GetCustomerById(customerId);
			if (customer != null)
			{
				_db.Customers.Remove(customer);
				_db.SaveChanges();
			}
		}
	}
}
