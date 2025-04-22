using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Models;
using Project_00.Services.Interfaces;

namespace Project_00.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public CategoryServices(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Category> AddCategory(CategoryDto request)
        {
            try
            {
                if (await _context.Categories.AnyAsync(c => c.Name == request.Name))
                    { return null; }
                var category = _mapper.Map<Category>(request);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Category> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null) return null;
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                if (categories is null) return null;
                return categories;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Category> GetCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null) return null;
                return category;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Category> UpdateCategory(int id, CategoryDto request)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null) return null;
                category.Name = request.Name;
                await _context.SaveChangesAsync();
                return category;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }
    }
}
