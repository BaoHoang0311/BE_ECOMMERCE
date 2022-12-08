using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
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
        public async Task<IActionResult> GetProductsById(int id)
        {

            var dulieu = await _productRepository.GetByIdAsync(id);



            if (dulieu == null) return NotFound();
            var dulieu_map = _mapper.Map<Product>(dulieu);

            return Ok(new
            {
                message = "GetProductsBy Idthanh cong",
                data = dulieu_map
            });
        }

        // api/Products/name?name=36a8b2df-749b-4eb8-a654-b37c5fa65181

        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        [HttpPost]
        public async Task<bool> CreateProduct(ProductDtos productVM)
        {

            var check = await _productRepository.AddAsync(productVM);
            return check;
        }
        //api/Products?id=36a8b2df-749b-4eb8-a654-b37c5fa65181
        //https://localhost:44381/api/Products?id=9dc0c168-a572-4269-b401-31c889158d35
        [HttpPut]
        public async Task<bool> UpdateProduct(ProductDtos productsDtos)
        {
            var check = await _productRepository.UpdateAsync(productsDtos);
            return check;
        }
        [HttpDelete]
        //https://localhost:44381/api/Orders/45a1bf78-aa7f-4434-90d3-ce033c71face
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteAsync(id);
            return Ok(new { message = "xoa thanh cong" });
        }
    }
}
