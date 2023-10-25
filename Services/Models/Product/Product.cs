using Services.Models.Cart;
using Services.Models.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.Products
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[StringLength(200)]
		public required string Name { get; set; }
		public required string Description { get; set; }
		[Required]
		[Column(TypeName = "money")]
		public required decimal Price { get; set; } = 0;
		public required long StockQuantity { get; set; } = 0;
		public required string Slug { get; set; }
		[Column("Path")]
		public string photoPath { get; set; } = string.Empty;
		public virtual ICollection<CartDetail>? CartDetails { get; set; }
		public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
		public virtual Category Category { get; set; }
	}
}
