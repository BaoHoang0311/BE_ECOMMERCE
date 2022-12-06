using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository services, IMapper mapper)
        {
            _orderRepository = services;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOrder()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDtos orderDtos)
        {
            try
            {
                await _orderRepository.AddOrderAsync(orderDtos);
                 return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
