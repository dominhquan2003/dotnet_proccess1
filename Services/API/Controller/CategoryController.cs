using Microsoft.AspNetCore.Mvc;
using Services.API.RequestEntities;
using Services.API.ResponseEntity;
using Services.Models.Products;
using Services.Repository;
using Services.Services;

namespace Services.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly CategoryService _categoryService;

		public CategoryController(MyDbContext db)
		{
			_categoryService = new CategoryService(db);
		}

		[HttpGet]
		public IActionResult GetCategories()
		{
			var result = new List<CategoryResponse>();
			foreach (var x in _categoryService.GetCategories())
			{
				result.Add(new CategoryResponse() { Id = x.Id, Name = x.Name });
			}
			return Ok(result);
		}

		[HttpPost]
		public IActionResult Create([FromBody] CategoryRequest category)
		{
			try
			{
				if (category == null)
					return BadRequest("Category object is null");

				var c = new Category { Id = category.Id, Name = category.Name };

				_categoryService.Create(c);
				return Ok("Category created successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut]
		public IActionResult Update(int id, [FromBody] CategoryRequest category)
		{
			try
			{
				if (category == null || id != category.Id)
					return BadRequest("Invalid category data");

				var c = new Category { Id = category.Id, Name = category.Name };

				_categoryService.Update(c);
				return Ok("Category updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var category = _categoryService.GetById(id);
				return Ok(new CategoryResponse() { Id = category.Id, Name = category.Name });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_categoryService.Delete(id);
				return Ok("Category deleted successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
