using Microsoft.EntityFrameworkCore;
using Services.Models.Cart;
using Services.Models.Customers;
using Services.Models.Order;
using Services.Models.Products;
using Services.Models.User;

namespace Services.Repository
{
	public class MyDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartDetail> CartDetails { get; set; }

		public MyDbContext(DbContextOptions options) : base(options)
		{

		}

		public MyDbContext() : base(new DbContextOptionsBuilder<MyDbContext>()
						.UseSqlServer("Data Source=.;Initial Catalog=dotnet_QT1;Integrated Security=True")
						.Options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=.;Initial Catalog=dotnet_QT1;Integrated Security=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
				.HasIndex(u => u.Phone)
				.IsUnique();
		}
	}
}
