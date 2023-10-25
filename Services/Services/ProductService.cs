using Services.Helpers;
using Services.Models.Products;
using Services.Repository;

namespace Services.Services
{
	public class ProductService
	{
		public ProductRepository productRepo;
		public CategoryRepository categoryRepo;

		public ProductService(MyDbContext db)
		{
			productRepo = new ProductRepository(db);
			categoryRepo = new CategoryRepository(db);
		}

		public List<Product> GetAll()
		{
			return productRepo.GetAllProduct().OrderBy(p => p.Price).ToList(); ;
		}

		public Product? GetById(int id)
		{

			var product = productRepo.GetProductById(id);
			return product;
		}

		public List<Product> GetProductsByPage(int pageNumber, int pageSize)
		{
			int skipCount = (pageNumber - 1) * pageSize;
			return productRepo.GetAllProduct().Skip(skipCount).Take(pageSize).ToList();
		}

		public IEnumerable<Product> FilterProducts(decimal? minPrice, decimal? maxPrice, int? minStockQuantity, int? maxStockQuantity, int? categoryId)
		{
			var query = productRepo.QueryTable();

			if (minPrice.HasValue)
				query = query.Where(p => p.Price >= minPrice);

			if (maxPrice.HasValue)
				query = query.Where(p => p.Price <= maxPrice);

			if (minStockQuantity.HasValue)
				query = query.Where(p => p.StockQuantity >= minStockQuantity);

			if (maxStockQuantity.HasValue)
				query = query.Where(p => p.StockQuantity <= maxStockQuantity);

			if (categoryId.HasValue)
				query = query.Where(p => p.Category.Id == categoryId);

			return query.ToList();
		}

		public bool CreateProduct(string name, string desc, decimal price, int stockQuan, int categoryId)
		{
			try
			{
				var product = new Product()
				{
					Name = name,
					Description = desc,
					Price = price,
					StockQuantity = stockQuan,
					Slug = Generate.GeneratedSlug(name),
					Category = categoryRepo.GetById(categoryId)
				};
				productRepo.Create(product);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Error: " + ex.Message);
			}
		}

		public bool UpdateProduct(int id, string name, string desc, decimal price, int stockQuan, int categoryId)
		{
			var product = new Product()
			{
				Id = id,
				Name = name,
				Description = desc,
				Price = price,
				StockQuantity = stockQuan,
				Slug = Generate.GeneratedSlug(name),
				Category = categoryRepo.GetById(categoryId)
			};
			try
			{
				productRepo.Update(product);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Error: " + ex.Message);
			}
		}

		public bool DeleteProduct(int id)
		{
			var p = productRepo.GetProductById(id);
			try
			{
				productRepo.Delete(p);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
