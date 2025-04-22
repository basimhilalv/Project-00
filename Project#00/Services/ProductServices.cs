using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Models;
using Project_00.Services.Interfaces;

namespace Project_00.Services
{
    public class ProductServices : IProductServices
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public ProductServices(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> AddProduct(ProductDto request)
        {
            try
            {
                if(await _context.Products.AnyAsync(p => p.Name == request.Name))
                {
                    return null;
                }
                var product = _mapper.Map<Product>(request);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while adding data", ex);
            }
        }

        public async Task<Product> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if(product == null)
                {
                    return null;
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while deleting", ex);
            }

        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id == id);
                if(product == null)
                {
                    return null;
                }
                return product;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var products = await _context.Products.Include(p=>p.Category).ToListAsync();
                if (products is null) return null;
                return products;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _context.Products
                    .Include(p=>p.Category)
                    .Where(p => p.Category.Name == category)
                    .ToListAsync();
            
                if (products is null) return null;
                return products;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Product> UpdateProduct(int id, ProductDto request)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null) return null;
                product.Price = request.Price;
                product.Name = request.Name;
                product.CategoryId = request.CategoryId;
                await _context.SaveChangesAsync();
                return product;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }
    }
}
