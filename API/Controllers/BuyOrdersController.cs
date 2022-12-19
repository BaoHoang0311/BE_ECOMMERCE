using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOrdersController : ControllerBase
    {
        private readonly IBuyOrderRepository _BuyorderRepository;
        private readonly IMapper _mapper;
        public BuyOrdersController(IBuyOrderRepository services, IMapper mapper)
        {
            _BuyorderRepository = services;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBuyOrder(string sortBy, int pageNumber, int pageSize)
        {
            try
            {
                var _result = await _BuyorderRepository.GetAllAsyncSortByIdAndPaging(sortBy, pageNumber, pageSize);

                var results = new results()
                {
                    statusCode = 200,
                    message = "GetAllBuyOrder thanh cong",
                    Data = _result,
                };

                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest("Khong ton tai danh sach BuyOrder");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyOrderbyOrderId(int id)
        {
            try
            {
                var listOrder = await _BuyorderRepository.GetBuyOrderbyOrderId(id);

                var results = new results()
                {
                    statusCode = 200,
                    message = "GetBuyOrderbyOrderId thanh cong",
                };
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBuyOrder(BuyOrderDtos buyorderDtos)
        {
            //var check = await _BuyorderRepository.AddBuyOrderAsync(buyorderDtos);
            var check = await _BuyorderRepository.AddBuyOrderAsync_1(buyorderDtos);
            if (check == true)
            {
                if (check == true)
                {
                    var results = new results()
                    {
                        statusCode = 200,
                        message = "AddBuyOrder thanh cong",
                    };
                    return Ok(results);
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyOrderbyOrderId(int id)
        {
            try
            {
                await _BuyorderRepository.DeleteAsync(id);

                var results = new results()
                {
                    statusCode = 200,
                    message = "DeleteBuyOrderbyOrderId thanh cong",
                };
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(BuyOrderDtos buyorderDtos)
        {
            var check = await _BuyorderRepository.UpdateBuyOrder(buyorderDtos);
            if (check == true)
            {
                var results = new results()
                {
                    statusCode = 200,
                    message = "DeleteBuyOrderbyOrderId thanh cong",
                };
                return Ok(results);
            }
            return BadRequest();
        }
    }
}
