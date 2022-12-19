using API.Dtos;
using API.Entites;
using API.Helpers;
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

        //[HttpGet]
        //public async Task<IActionResult> GetAllOrder()
        //{
        //    var list = await _orderRepository.GetQuery().Include(m => m.OrderDetails).ToListAsync();
        //    return Ok(list);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllOrder (string sortBy, int pageNumber, int pageSize)
        {
            try
            {
                var _result = await _orderRepository.GetAllAsyncSortByIdAndPaging(sortBy, pageNumber, pageSize, m=>m.customer,m=>m.OrderDetails);

                var results = new results()
                {
                    statusCode = 200,
                    message = "GetAllProduct success",
                    Data = _result,
                };

                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest("Not find list of Order");
            }
        }
       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderbyOrderId(int id)
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
            //var check = await _orderRepository.AddOrderAsync(orderDtos);
            var check = await _orderRepository.AddOrderAsync_1(orderDtos);
            if (check == true)
            {
                var results = new results()
                {
                    statusCode = 200,
                    message = "AddOrder success",
                };
                return Ok(results);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderbyOrderId(int id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);

                var results = new results()
                {
                    statusCode = 200,
                    message = "DeleteOrderbyOrderId success",
                };
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDtos Order)
        {
            var check = await _orderRepository.UpdateOrder(Order);
            if (check == true)
            {
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateProduct success",
                };
                return Ok(results);
            }
            return BadRequest();
        }
    }
}
