using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareAPI.ViewModels;
using ProductMiddlewareDataAcces.Interfaces;

namespace ProductMiddlewareAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string ProductsCachKey = "ProductsCach";

        public ProductService(IProductRepository productRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProductsAsync()
        {        

            if (!_memoryCache.TryGetValue(ProductsCachKey, out IEnumerable<ProductVM> products))
            {
                var productData = await _productRepository.GetAllProductsAsync();
                products = _mapper.Map<IEnumerable<ProductVM>>(productData);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(ProductsCachKey, products, cacheEntryOptions);
            }
            return products;
        }

        public async Task<ProductVM> GetProductByIdAsync(int id)
        {
            var cacheKey = $"product_{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out ProductVM product))
            {
                var productData = await _productRepository.GetProductByIdAsync(id);
                if (productData == null)
                {
                    return null;
                }

                product = _mapper.Map<ProductVM>(productData);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, product, cacheEntryOptions);
            }
            return product;
        }
        public async Task<IEnumerable<ProductVM>> FilterProductAsync(string category, decimal? minPrice, decimal? maxPrice)
        {
            var cacheKey = $"products_{category}_{minPrice}_{maxPrice}";

            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ProductVM> products))
            {
                var productData = await _productRepository.FilterProductAsync(category, minPrice, maxPrice);
                products = _mapper.Map<IEnumerable<ProductVM>>(productData);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, products, cacheEntryOptions);
            }

            return products;
        }

        public async Task<IEnumerable<ProductVM>> SearchProductAsync(string query)
        {
            var cacheKey = $"products_search_{query}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ProductVM> products))
            {
                var productsData = await _productRepository.SearchProductsAsync(query);
                products = _mapper.Map<IEnumerable<ProductVM>>(productsData);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, products, cacheEntryOptions);
            }

            return products;
        }
    }
}
