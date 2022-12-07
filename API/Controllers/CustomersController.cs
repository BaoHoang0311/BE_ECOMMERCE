using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository services, IMapper mapper)
        {
            _customerRepository = services;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            #region Include
            // dulieu = _product.Where(condition).Include ( a => a.b).Include (a=>a.c)
            //var spec = new Productwith_Include_Condition();
            //var dulieu = await _productRepository.ListAsync(spec);
            #endregion

            var dulieu = await _customerRepository.GetAllAsync();

            if (dulieu == null) return NotFound();
            var dulieu_map = dulieu;
            // return list with special
            return Ok(new
            {
                message = "GetCustomers thanh cong",
                data = dulieu
            });

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomesrsById(string id)
        {
            //var spec = new Productwith_Include_Condition(id);
            //var dulieu = await _productRepository.GetEntityWithSpec(spec);

            var dulieu = await _customerRepository.GetByIdAsync(id);



            if (dulieu == null) return NotFound();
            var dulieu_map = dulieu;

            return Ok(new
            {
                message = "GetCustomesrsById thanh cong",
                data = dulieu_map
            });
        }

        //[HttpGet("name")]
        //public async Task<IActionResult> GetCustomersByName(string name)
        //{
        //    var dulieu = await _customerRepository.GetByNameAsync(name);
        //    if (dulieu == null) return NotFound();
        //    var dulieu_map = dulieu;

        //    return Ok(new
        //    {
        //        message = "GetCustomersByName thanh cong",
        //        data = dulieu_map
        //    });
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomrer(string id)
        {
            try
            {
                await _customerRepository.DeleteAsync(id);
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
        [HttpPost]
        public async Task<IActionResult> CreateCustomrer(CustomerDtos customerdtos)
        {
            try
            {
                await _customerRepository.AddAsync(customerdtos);
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
        [HttpPut]
        //https://localhost:44381/api/Customers?id=21862dcd-b42c-4468-9b8d-f86d9f5fcc6f
        public async Task<IActionResult> UpdateProduct(string id, CustomerDtos customersDtos)
        {
            try
            {
                await _customerRepository.UpdateAsync(id, customersDtos);
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
    }
}
