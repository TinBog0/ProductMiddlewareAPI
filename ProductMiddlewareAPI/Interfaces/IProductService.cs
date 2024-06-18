using ProductMiddlewareAPI.ViewModels;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVM>> GetAllProductsAsync();

        Task<ProductVM> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductVM>> FilterProductAsync(string category, decimal? minPrice, decimal? macPrice);

        Task<IEnumerable<ProductVM>> SearchProductAsync(string query);
    }
}
