using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
