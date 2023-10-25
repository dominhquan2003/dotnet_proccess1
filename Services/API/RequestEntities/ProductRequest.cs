﻿namespace Services.API.RequestEntity
{
	public class ProductRequest
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public int CategoryId { get; set; }
	}
}
