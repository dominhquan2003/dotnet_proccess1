using Services.Models.Products;
using Services.Repository;

namespace Services.Services
{
	public class CategoryService
	{
		private CategoryRepository repository;
		public CategoryService(MyDbContext db)
		{
			repository = new CategoryRepository(db);
		}

		public List<Category> GetCategories()
		{
			return repository.GetAll();
		}
		public Category GetById(int id)
		{
			return repository.GetById(id);
		}

		public void Create(Category category)
		{
			repository.Create(category);
		}

		public void Update(Category category)
		{
			repository.Update(category);
		}

		public void Delete(int id)
		{
			repository.Delete(id);
		}
	}
}
