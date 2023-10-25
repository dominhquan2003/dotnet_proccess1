using Services.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Order
{
	public class OrderDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public virtual required Order Order { get; set; }
		[Column("Product")]
		[Required]
		public virtual required Product Product { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
	}
}
