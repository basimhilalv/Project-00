using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class ProductServices : IProductServices
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public ProductServices(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> AddProduct(ProductDto request)
        {
            if(await _context.Products.AnyAsync(p => p.Name == request.Name))
            {
                return null;
            }
            var product = _mapper.Map<Product>(request);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id == id);
            if(product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Include(p=>p.Category).ToListAsync();
            if (products is null) return null;
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var products = await _context.Products
                .Include(p=>p.Category)
                .Where(p => p.Category.Name == category)
                .ToListAsync();
            
            if (products is null) return null;
            return products;
        }

        public async Task<Product> UpdateProduct(int id, ProductDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null) return null;
            product.Price = request.Price;
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
