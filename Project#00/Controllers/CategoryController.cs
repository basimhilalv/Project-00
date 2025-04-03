using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_00.Models;
using Project_00.Services;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryServices.GetCategories();
            if (categories is null) return NotFound("There are no categories");
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryServices.GetCategory(id);
            if (category is null) return NotFound("Category Not Found");
            return Ok(category);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Category>> AddCategory(CategoryDto request)
        {
            var create = await _categoryServices.AddCategory(request);
            if (create is null) return BadRequest("Category already exists");
            return Ok(create);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryDto request)
        {
            var update = await _categoryServices.UpdateCategory(id, request);
            if (update is null) return BadRequest("Category not found");
            return Ok(update);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var delete = await _categoryServices.DeleteCategory(id);
            if (delete is null) return NotFound("Category not found");
            return NoContent();
        }
    }
}
