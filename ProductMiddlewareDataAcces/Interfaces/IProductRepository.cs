using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareDataAcces.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> FilterProductAsync(string category, decimal? minPrice, decimal? maxPrice);
    }
}
