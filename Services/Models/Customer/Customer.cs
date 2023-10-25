using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Customers
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public required string Name { get; set; }
		public required string Email { get; set; }
		[Required]
		public required string Phone { get; set; }
		[Required]
		public string? Address { get; set; }
		public required string Password { get; set; }
		public virtual Models.Cart.Cart? Cart { get; set; }
	}
}
