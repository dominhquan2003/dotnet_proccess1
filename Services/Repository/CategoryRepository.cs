using Services.Models.Products;

namespace Services.Repository
{
	public class CategoryRepository
	{
		private readonly MyDbContext _db;

		public CategoryRepository(MyDbContext db)
		{
			_db = db;
		}


		public List<Category> GetAll()
		{
			return _db.Categories.ToList();
		}

		public Category GetById(int id)
		{
			var category = _db.Categories?.FirstOrDefault(c => c.Id == id);
			if (category == null)
			{
				throw new Exception("Category not found.");
			}
			return category;
		}

		public void Create(Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
		}

		public void Update(Category category)
		{
			var existingCategory = GetById(category.Id);

			if (existingCategory != null)
			{
				existingCategory.Name = category.Name;
				_db.Categories.Update(existingCategory);
				_db.SaveChanges();
			}
		}

		public void Delete(int id)
		{
			var cart = this.GetById(id);
			_db.Categories.Remove(cart);
			_db.SaveChanges();
		}
	}
}
