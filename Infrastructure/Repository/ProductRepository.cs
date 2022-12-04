using AutoMapper;
using Core.Entites;
using Core.Interfaces;
using Core.ViewModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(MyDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductVM entity)
        {
            var data = await _context.products.FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (data == null)
            {
                var pro = _mapper.Map<Product>(entity);
                pro.Id = Guid.NewGuid().ToString();
                pro.PictureUrl= 
                pro.productOwner = "";
                pro.CreatedDate = DateTime.Now;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                await _context.products.AddAsync(pro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(string id, ProductVM entity)
        {
            var data = await _context.products.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if(data != null)
            {
                var pro = _mapper.Map<Product>(entity);
                pro.Id = data.Id;
                pro.productOwner = "";
                pro.CreatedDate = data.CreatedDate;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                _context.products.Update(pro);
                await _context.SaveChangesAsync();
            }

        }
    }
}
