using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> FilterProductAsync(string category, decimal? minPrice, decimal? macPrice);

        Task<IEnumerable<Product>> SearchProductAsync(string query);
    }
}
