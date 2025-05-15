using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.ProductService
{
    public interface IProductServices
    {
        public Task<Product> GetProduct(int id);
        public Task<IEnumerable<Product>> GetProducts();
        public Task<IEnumerable<Product>> GetProductsByCategory(string category);
        public Task<Product> AddProduct(ProductDto request);
        public Task<Product> UpdateProduct(int id, ProductDto request);
        public Task<Product> DeleteProduct(int id);
    }
}
