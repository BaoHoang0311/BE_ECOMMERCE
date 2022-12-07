using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderbyIdCus(string id)
        {
            try
            {
                var listOrder = await _orderRepository.GetOrderbyIdCus(id);

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
        public async Task<IActionResult> AddOrder(OrderDtos orderDtos)
        {
            try
            {
                await _orderRepository.AddOrderAsync(orderDtos);
                 return Ok(new {message= "AddOrder thanh cong" });
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderbyIdCus(string id)
        {
            try
            {
                 await _orderRepository.DeleteAsync(id);

                return Ok(
                    new
                    {
                        message = "DeleteOrderbyIdCus thanh cong",
                    });

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
