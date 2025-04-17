using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.Interfaces
{
    public interface ICategoryServices
    {
        public Task<Category> GetCategory(int id);
        public Task<IEnumerable<Category>> GetCategories();
        public Task<Category> AddCategory(CategoryDto request);
        public Task<Category> UpdateCategory(int id, CategoryDto request);
        public Task<Category> DeleteCategory(int id);
    }
}
