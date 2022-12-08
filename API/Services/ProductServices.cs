using API.Data;
using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductServices : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public ProductServices(MyDbContext context, IMapper mapper) :base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductDtos entity)
        {
            var data = await _context.Products.FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (data == null)
            {
                var pro = _mapper.Map<Product>(entity);

                pro.productOwner = "";

                pro.CreatedBy = "";
                pro.CreatedDate = DateTime.Now;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                await _context.Products.AddAsync(pro);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(ProductDtos entity)
        {
            var data = await _context.Products.FirstOrDefaultAsync(m => m.Id == entity.Id);
            if(data != null)
            {
                data.ModifiedDate = DateTime.Now;
                data.FullName = entity.FullName;
                data.Amount = entity.Amount;

                _context.Products.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
