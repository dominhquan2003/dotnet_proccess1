namespace Services.API.ResponseEntity
{
	public class CartDetailResponse
	{
		public int Id { get; set; }
		public required ProductDTO Product { get; set; }
		public int Quatity { get; set; }
	}
}
