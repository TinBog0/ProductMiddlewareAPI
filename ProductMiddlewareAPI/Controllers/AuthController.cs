using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProductMiddlewareDataAccess.Models;
using System.Text;

namespace ProductMiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var json = new JObject
            {
                {"username", request.Username },
                {"password", request.Password}
            };

            var response = await _httpClient.PostAsync("https://dummyjson.com/auth/login", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(responseContent)["token"].ToString();

            return Ok(new { Token = token });
        }
    }
}
