using AutoMapper;
using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareAPI.ViewModels;
using ProductMiddlewareDataAcces.Interfaces;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<ProductVM> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ProductVM>(product);
        }
        public async Task<IEnumerable<ProductVM>> FilterProductAsync(string category, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.FilterProductAsync(category, minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<IEnumerable<ProductVM>> SearchProductAsync(string query)
        {
            var products = await _productRepository.SearchProductsAsync(query);
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }
    }
}
