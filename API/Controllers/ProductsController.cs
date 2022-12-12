using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
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
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository services, IMapper mapper)
        {
            _productRepository = services;
            _mapper = mapper;
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

        [HttpGet("get-pro")]
        public async Task<IActionResult> GetAllProduct(string sortBy, string searchString, int pageNumber, int pageSize)
        {
            try
            {
                var _result = await _productRepository.GetAllAsyncSortById(sortBy);
                _result = _productRepository.GetAllAsyncSearchandPaging(_result, searchString, pageNumber, pageSize);

                var results = new results()
                {
                    statusCode = 200,
                    message = "GetAllProduct thanh cong",
                    Data = _result,
                };

                return Ok(results);

            }
            catch (Exception)
            {
                return BadRequest("Khong ton tai danh sach san pham");
            }
        }

        // api/Products/3
        [HttpGet("get-pro/{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {

            var dulieu = await _productRepository.GetByIdAsync(id);

            if (dulieu == null) return NotFound();

            var results = new results()
            {
                statusCode = 200,
                message = "GetProductsById thanh cong",
                Data = dulieu,
            };

            return Ok(results);
        }

        // api/Products/name?name=3

        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpPost("add-pro")]
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
                        message = "CreateProduct thanh cong",
                    };
                    return Ok(results);
                }
                return BadRequest("CreateProduct khong thanh cong");
            }
            // ko nhap vao id
            catch
            {
                return BadRequest("CreateProduct khong thanh cong");
            }
        }
        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181

        //https://localhost:44381/api/Products?id=9
        [HttpPut("put-pro")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var data = await _productRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == product.Id);
                if (data != null)
                {
                    await _productRepository.UpdateAsync(product);

                    var results = new results()
                    {
                        statusCode = 200,
                        message = "UpdateProduct thanh cong",
                    };

                    return Ok(results);
                }
                return BadRequest("UpdateProduct khong thanh cong");
            }
            catch
            {
                return BadRequest("UpdateProduct khong thanh cong");
            }

        }

        [HttpDelete("del-pro")]
        //https://localhost:44381/api/Orders/4
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateProduct thanh cong",
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
