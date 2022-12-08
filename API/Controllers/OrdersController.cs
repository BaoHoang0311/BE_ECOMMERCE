using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetOrder()
        {
            var list = await _orderRepository.GetQuery().Include(m=>m.OrderDetails).ToListAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderbyOrderId (string id)
        {
            try
            {
                var listOrder = await _orderRepository.GetOrderbyOrderId(id);

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
        public async Task<IActionResult> DeleteOrderbyOrderId (string id)
        {
            try
            {
                 await _orderRepository.DeleteAsync(id);

                return Ok(
                    new
                    {
                        message = "DeleteOrderbyOrderId  thanh cong",
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<bool> Update(OrderDtos Order) 
        {
            var check = await _orderRepository.UpdateOrder(Order);
            if(check==true)
                return true;
            return false;
        }
    }
}
