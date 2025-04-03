using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _context;

        public CategoryServices(IMapper mapper, UserDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Category> AddCategory(CategoryDto request)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == request.Name))
                { return null; }
            var category = _mapper.Map<Category>(request);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) return null;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories is null) return null;
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) return null;
            return category;
        }

        public async Task<Category> UpdateCategory(int id, CategoryDto request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) return null;
            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
