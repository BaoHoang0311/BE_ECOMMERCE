using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOrdersController : ControllerBase
    {
        private readonly IBuyOrderRepository _BuyorderRepository ;
        private readonly IMapper _mapper;
        public BuyOrdersController(IBuyOrderRepository services, IMapper mapper)
        {
            _BuyorderRepository = services;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBuyOrder()
        {
            var list = await _BuyorderRepository.GetQuery().Include(m =>m.BuyOrderDetails ).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyOrderbyOrderId(int id)
        {
            try
            {
                var listOrder = await _BuyorderRepository.GetBuyOrderbyOrderId(id);

                return Ok(
                    new
                    {
                        message = "Ok",
                        data = listOrder,
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(BuyOrderDtos buyorderDtos)
        {
            //var check = await _BuyorderRepository.AddBuyOrderAsync(buyorderDtos);
            var check = await _BuyorderRepository.AddBuyOrderAsync_1(buyorderDtos);
            if (check == true)
            {
                return Ok(new { message = "AddOrder thanh cong" });

            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyOrderbyOrderId(int id)
        {
            try
            {
                await _BuyorderRepository.DeleteAsync(id);

                return Ok(
                    new
                    {
                        message = "DeleteBuyOrderbyOrderId  thanh cong",
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<bool> Update(BuyOrderDtos buyorderDtos)
        {
            var check = await _BuyorderRepository.UpdateBuyOrder(buyorderDtos);
            if (check == true)
                return true;
            return false;
        }
    }
}
