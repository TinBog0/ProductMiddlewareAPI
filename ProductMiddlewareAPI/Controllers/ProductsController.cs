using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/products
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            _logger.LogInformation("Fetching all products");

            try
            {
                var products = await _productService.GetAllProductsAsync();

                _logger.LogInformation("Products succesfully fetched.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching products: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/products/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute]int id)
        {
            _logger.LogInformation($"Fetching product with id {id}");
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Products succesfully fetched.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching products: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Filters products by category and price range.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/products/filter?category=furniture?minPrice=100?maxPrice=1000
        ///
        /// </remarks>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts([FromQuery] string category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category is required");
            }

            _logger.LogInformation($"Fetching products in category :{category} price from {minPrice} to {maxPrice}");

            try
            {
                var products = await _productService.FilterProductAsync(category, minPrice, maxPrice);

                _logger.LogInformation("Products succesfully fetched.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching products: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Searches products by name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/products/search?query=laptop
        ///
        /// </remarks>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(string query)
        {
            _logger.LogInformation($"Searching products with query: {query}");
            try
            {
                var products = await _productService.SearchProductAsync(query);

                _logger.LogInformation("Products succesfully fetched.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching products: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
