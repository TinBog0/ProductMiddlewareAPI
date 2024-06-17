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
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred whileretrieving products.", ex);
            }
        }

        //public async Task<Product> GetProductByIdAsync(int id)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync()

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
