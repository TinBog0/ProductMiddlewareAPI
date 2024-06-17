using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductMiddlewareDataAcces.Interfaces;
using ProductMiddlewareDataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMiddlewareDataAcces.Repositories
{
    public class ApiProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _productApiUrl;

        public ApiProductRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _productApiUrl = configuration["ApiSettings:ProductApiUrl"];
        }

        public async Task<IEnumerable<Product>> FilterProductAsync(string category, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_productApiUrl}/category/{category}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var productResponse = JsonConvert.DeserializeObject<ApiProductResponse>(content);

                var filteredProducts = productResponse.Products.AsQueryable();

                if (minPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);  
                }

                return filteredProducts.ToList();
            }
            catch (HttpRequestException ex )
            {
                throw new ApplicationException($"An error occurred while filtering products in category {category}.", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_productApiUrl}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var productsResponse = JsonConvert.DeserializeObject<ApiProductResponse>(content);

                return productsResponse.Products;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("An error occurred while retrieving products.", ex);
            }
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {

            try
            {
                var response = await _httpClient.GetAsync($"{_productApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(content);

                return product;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the product with Id {id}.", ex);
            }
        }
    }
}
