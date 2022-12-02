using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _services;
        public ProductsController(IProductService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var data = await _services.GetAllAsync();
            return Ok(data);
                
        }
        [HttpGet("id")]
        public async  Task<IActionResult> GetProductsById(string id)
        {
            return Ok(
                new
                {
                    message = "thanh cong",
                    data = await _services.GetByIdAsync(id)
                });
        }
    }
}
