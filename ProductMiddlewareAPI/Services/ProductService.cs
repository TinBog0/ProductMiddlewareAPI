using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareDataAcces.Interfaces;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> FilterProductAsync(string category, decimal? minPrice, decimal? macPrice)
        {
            return _productRepository.FilterProductAsync(category, minPrice, macPrice);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _productRepository.GetProductByIdAsync(id);
        }

        public Task<IEnumerable<Product>> SearchProductAsync(string query)
        {
            return _productRepository.SearchProductsAsync(query);
        }
    }
}
