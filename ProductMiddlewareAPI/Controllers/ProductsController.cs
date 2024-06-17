using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute]int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts([FromQuery] string category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category is required");
            }

            try
            {
                var products = await _productService.FilterProductAsync(category, minPrice, maxPrice);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(string query)
        {
            try
            {
                var products = await _productService.SearchProductAsync(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
