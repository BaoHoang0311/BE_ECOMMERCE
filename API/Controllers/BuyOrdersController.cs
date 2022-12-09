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
        public async Task<IActionResult> GetOrder()
        {
            var list = await _BuyorderRepository.GetQuery().Include(m =>m.BuyOrderDetails ).ToListAsync();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBuyOrder(BuyOrder BuyOrder)
        {
            //_BuyorderRepository.AddBuyOrderAsync();
            return Ok();
        }
    }
}
