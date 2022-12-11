using API.Dtos;
using API.Entites;
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
                return Ok(_result);
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
            var dulieu_map = dulieu;

            return Ok(new
            {
                message = "GetProductsBy Idthanh cong",
                data = dulieu_map
            });
        }

        // api/Products/name?name=3

        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpPost("add-pro")]
        public async Task<bool> CreateProduct(ProductDtos productDtos)
        {
            // ko nhap vao id
            var data = await _productRepository.GetQuery().FirstOrDefaultAsync(m => m.FullName == productDtos.FullName);
            if (data == null)
            {
                var cus = _mapper.Map<Product>(productDtos);
                await _productRepository.AddAsync(cus);
                return true;
            }
            return false;
        }
        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        //https://localhost:44381/api/Products?id=9
        [HttpPut("put-pro")]
        public async Task<bool> UpdateProduct(Product product)
        {
            var data = await _productRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == product.Id);
            if (data != null)
            {
                await _productRepository.UpdateAsync(product);
                return true;
            }
            return false;

        }

        [HttpDelete("del-pro")]
        //https://localhost:44381/api/Orders/4
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return Ok(new { message = "xoa thanh cong" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
