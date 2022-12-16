using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {

            var dulieu = await _orderDetailRepository.GetAllAsync();

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetProducts thanh cong",
                data = dulieu
            });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetails(OrderDetailDtos orderDetaildtos)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetaildtos);
            var data = await _orderDetailRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == orderDetaildtos.Id);
            if (data != null)
            {
                // sau này có FE lấy thông tin nên ko cần
                orderDetail.OrderId = data.OrderId;
                orderDetail.CreatedDate = data.CreatedDate;
                await _orderDetailRepository.UpdateAsync(orderDetail);
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateOrderDetails thanh cong",
                };
                return Ok(results);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await _orderDetailRepository.DeleteAsync(id);
            return Ok(new 
            { 
                message = "DeleteOrderDetail thanh cong"
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {

            var dulieu = await _orderDetailRepository.GetByIdAsync(id);

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetProductsBy Idthanh cong",
                data = dulieu
            });
        }
    }
}
