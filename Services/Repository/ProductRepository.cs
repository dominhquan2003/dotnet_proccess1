using Services.Models.Products;

namespace Services.Repository
{
	public class ProductRepository
	{
		private readonly MyDbContext _db;

		public ProductRepository(MyDbContext db)
		{
			_db = db;
		}
		// get all product
		public List<Product> GetAllProduct()
		{
			return _db.Products.ToList();
		}
		// get by id
		public Product GetProductById(int Id)
		{
			return _db.Products.FirstOrDefault(p => p.Id == Id);
		}
		// update
		public void Update(Product product)
		{

			var p = _db.Products.FirstOrDefault(p => p.Id == product.Id);

			if (p == null)
			{
				throw new Exception("Not found!");
			}

			p.Name = product.Name;
			p.Description = product.Description;
			p.Price = product.Price;
			p.StockQuantity = product.StockQuantity;
			p.Category = product.Category;

			_db.SaveChanges();
		}
		// delete
		public void Delete(Product product)
		{
			_db.Remove(product);
			_db.SaveChanges();
		}
		// create
		public void Create(Product product)
		{
			_db.Products.Add(product);
			_db.SaveChanges();
		}
		// all product can be used by linq
		public IQueryable<Product> QueryTable()
		{
			return _db.Products.AsQueryable();
		}
	}
}
