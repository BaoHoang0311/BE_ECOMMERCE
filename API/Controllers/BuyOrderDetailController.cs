using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOrderDetailController : ControllerBase
    {
        private readonly IBuyOrderDetailRepository _buyOrderDetailRepository;
        private readonly IMapper _mapper;
        public BuyOrderDetailController(IBuyOrderDetailRepository buyOrderDetailRepository, IMapper mapper)
        {
            _buyOrderDetailRepository= buyOrderDetailRepository;
            _mapper = mapper;
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
        [HttpPut]
        public async Task<IActionResult> UpdateBuyOrderDetails(BuyOrderDetailDtos buyorderDetaildtos)
        {
            var buyorderDetail = _mapper.Map<BuyOrderDetail>(buyorderDetaildtos);
            var data = await _buyOrderDetailRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == buyorderDetaildtos.Id);
            if (data != null)
            {
                // sau này có FE lấy thông tin nên ko cần
                buyorderDetail.BuyOrderId = data.BuyOrderId;
                buyorderDetail.CreatedDate = data.CreatedDate;
                await _buyOrderDetailRepository.UpdateAsync(buyorderDetail);
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateBuyOrderDetails thanh cong",
                };
                return Ok(results);
            }
            return BadRequest();
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

            var dulieu = await _buyOrderDetailRepository.GetByIdAsync(id);

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetBuyOrderDetailById thanh cong",
                data = dulieu
            });
        }
    }
}
