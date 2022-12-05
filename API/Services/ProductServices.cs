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
        public ProductServices(MyDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductDtos entity)
        {
            var data = await _context.Products.FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (data == null)
            {
                var pro = _mapper.Map<Product>(entity);
                pro.Id = Guid.NewGuid().ToString();
                pro.productOwner = "";
                pro.CreatedDate = DateTime.Now;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                await _context.Products.AddAsync(pro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(string id, ProductDtos entity)
        {
            var data = await _context.Products.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if(data != null)
            {
                var pro = _mapper.Map<Product>(entity);
                pro.Id = data.Id;
                pro.productOwner = "";
                pro.CreatedDate = data.CreatedDate;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                _context.Products.Update(pro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
