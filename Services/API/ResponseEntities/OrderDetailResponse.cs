namespace Services.API.ResponseEntity
{
	public class OrderDetailResponse
	{
		public int Id { get; set; }
		public ProductDTO Product { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
