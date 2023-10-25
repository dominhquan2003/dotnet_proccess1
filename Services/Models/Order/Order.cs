using Services.Models.Customers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Order
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public required DateTime OrderDate { get; set; }

		[Column(TypeName = "varchar")]
		[MaxLength(100)]
		public required string ReferenceId { get; set; } 

		[Column(TypeName = "money")]
		public decimal TotalAmount { get; set; }
		public bool? Status { get; set; }
		public virtual required Customer Customer { get; set; }
	}
}
	