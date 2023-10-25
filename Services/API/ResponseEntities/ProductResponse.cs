using Services.Models.Products;

namespace Services.API.ResponseEntity
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public required string? Name { get; set; }
		public required string? Description { get; set; }
		public decimal Price { get; set; }
		public required long StockQuatity { get; set; }
		public required string Slug { get; set; }
		public required CategoryResponse Category { get; set; }
		public ProductDTO(Product p)
		{
			Id = p.Id;
			Name = p.Name;
			Description = p.Description;
			Price = p.Price;
			StockQuatity = p.StockQuantity;
			Slug = p.Slug;
			Category = new CategoryResponse() { Id = p.Category.Id, Name = p.Category.Name };
		}
		public string GenerateSlug()
		{
			string slug = Name?.ToLower().Replace(" ", "-");

			slug = new string(slug?.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());

			return slug?.Trim('-'); ;
		}
	}
}
