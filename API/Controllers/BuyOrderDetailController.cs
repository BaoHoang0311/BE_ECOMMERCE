using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOrderDetailController : ControllerBase
    {
        private readonly IBuyOrderDetailRepository _buyOrderDetailRepository;
        public BuyOrderDetailController(IBuyOrderDetailRepository buyOrderDetailRepository)
        {
            _buyOrderDetailRepository= buyOrderDetailRepository;    
        }
        [HttpGet]
        public async Task<IActionResult> GetBuyOrderDetails()
        {

            var dulieu = await _buyOrderDetailRepository.GetAllAsync();

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetBuyOrderDetails thanh cong",
                data = dulieu
            });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBuyOrderDetail(int id)
        {
            await _buyOrderDetailRepository.DeleteAsync(id);
            return Ok(new { message = "xoa thanh cong" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyOrderDetailById(int id)
        {

            var dulieu = await _buyOrderDetailRepository.GetAllListById(id);

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetBuyOrderDetailById thanh cong",
                data = dulieu
            });
        }
    }
}
