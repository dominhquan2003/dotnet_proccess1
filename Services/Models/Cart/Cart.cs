using Services.Models.Customers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Cart
{
	public class Cart
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[ForeignKey("CustomerId")]
		public virtual required Customer Customer { get; set; }
		[ForeignKey("CartDetail")]
		public DateTime CreatedAt { get; set; }
	}
}
