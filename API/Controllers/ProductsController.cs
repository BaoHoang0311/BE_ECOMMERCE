using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Helpers.Nlog;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductServices _productRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public ProductsController(IProductServices services, IMapper mapper, ILoggerManager logger)
        {
            _productRepository = services;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet("get-pro")]
        //public async Task<IActionResult> GetProducts()
        //{
        //    var dulieu = await _productRepository.GetAllAsync();
        //    if (dulieu == null) return NotFound();
        //    // return list with special
        //    return Ok(new
        //    {
        //        message = "GetProducts thanh cong",
        //        data = dulieu
        //    }) ;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllProduct(string sortBy, int? pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInfo("Get All Product");
                var _result = await _productRepository.GetAllAsyncSortByIdAndPaging(sortBy, pageNumber, pageSize);

                var results = new results()
                {
                    statusCode = 200,
                    message = "GetAllProduct success",
                    Data = _result.results,
                };

                return Ok(results);

            }
            catch (Exception)
            {
                _logger.LogWarning("Co loi xay ra ");
                return BadRequest("Not find list of Products");
            }
        }

        // api/Products/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {

            var dulieu = await _productRepository.GetByIdAsync(id);

            if (dulieu == null) return NotFound();

            var results = new results()
            {
                statusCode = 200,
                message = "GetProductsById success",
                Data = dulieu,
            };

            return Ok(results);
        }

        // api/Products/name?name=3

        //api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDtos productDtos)
        {
            try
            {
                var data = await _productRepository.GetQuery().FirstOrDefaultAsync(m => m.FullName == productDtos.FullName);
                if (data == null)
                {
                    var cus = _mapper.Map<Product>(productDtos);
                    await _productRepository.AddAsync(cus);

                    var results = new results()
                    {
                        statusCode = 200,
                        message = "CreateProduct success",
                    };
                    return Ok(results);
                }
                return BadRequest("CreateProduct failed");
            }
            // ko nhap vao id
            catch
            {
                return BadRequest("CreateProduct failed");
            }
        }

        //api/Products

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var data = await _productRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == product.Id);
                if (data != null)
                {
                    product.CreatedDate = data.CreatedDate;
                    await _productRepository.UpdateAsync(product);

                    var results = new results()
                    {
                        statusCode = 200,
                        message = "UpdateProduct success",
                    };

                    return Ok(results);
                }
                return BadRequest("UpdateProduct failed");
            }
            catch
            {
                return BadRequest("UpdateProduct failed");
            }

        }

        // https://localhost:44381/api/Product?id=9
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateProduct success",
                };
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
