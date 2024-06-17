using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareDataAcces.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);
    }
}
