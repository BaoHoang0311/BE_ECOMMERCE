using AutoMapper;
using Core.Entites;
using Core.Interfaces;
using Core.Specifications;
using Core.ViewModel;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            #region Include
            // dulieu = _product.Where(condition).Include ( a => a.b).Include (a=>a.c)
            //var spec = new Productwith_Include_Condition();
            //var dulieu = await _productRepository.ListAsync(spec);
            #endregion

            var dulieu = await _productRepository.GetAllAsync();

            if (dulieu == null) return NotFound();
            // return list with special
            var dulieu_map = _mapper.Map<IEnumerable<Product>>(dulieu);
            return Ok(new
            {
                message = "GetProducts thanh cong",
                data = dulieu_map
            });

        }
        // api/Products/36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(string id)
        {
            //var spec = new Productwith_Include_Condition(id);
            //var dulieu = await _productRepository.GetEntityWithSpec(spec);

            var dulieu = await _productRepository.GetByIdAsync(id);

            

            if (dulieu == null) return NotFound();
            var dulieu_map = _mapper.Map<IEnumerable<Product>>(dulieu);

            return Ok(new 
            {
                message = "GetProductsBy Idthanh cong",
                data = dulieu_map
            });
        }
        // api/Products/name?name=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpGet("name")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var dulieu = await _productRepository.GetByNameAsync(name);
            if (dulieu == null) return NotFound();
            var dulieu_map = _mapper.Map<IEnumerable<Product>>(dulieu);

            return Ok(new
            {
                message = " GetProductByName thanh cong",
                data = dulieu_map
            });
        }
        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductVM productVM)
        {
            try
            {
                await _productRepository.AddAsync(productVM);
                return Ok(
                    new
                    {
                        tt = "Create Successfull"
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(string id, ProductVM productVM)
        {
            try
            {
                await _productRepository.UpdateAsync(id, productVM);
                return Ok(
                    new
                    {
                        tt = "Update Successfull"
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return Ok(
                    new
                    {
                        message = "Delete Successfull"
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
