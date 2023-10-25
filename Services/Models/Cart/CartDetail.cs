using Services.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Cart
{
	public class CartDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("ProductId")]
		public virtual required Product Product { get; set; }
		[Required]
		public int Quantity { get; set; }
		[ForeignKey("CartId")]
		public int CartId { get; set; }
		public virtual Cart? Cart { get; set; }

	}
}
