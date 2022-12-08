using API.Data;
using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class CustomerServices : EntityBaseRepository<Customer>, ICustomerRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public CustomerServices(MyDbContext context, IMapper mapper) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CustomerDtos entity)
        {
            var data = await _context.Customers.FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (data == null)
            {
                var pro = _mapper.Map<Customer>(entity);
                pro.Id = Guid.NewGuid().ToString();
                pro.FullName = entity.FullName;
                pro.Email= entity.Email;

                pro.CreatedBy = "";
                pro.CreatedDate = DateTime.Now;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";

                await _context.Customers.AddAsync(pro);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync( CustomerDtos entity)
        {
            var data = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == entity.Id);
            if (data != null)
            {
                var pro = _mapper.Map<Customer>(entity);
                
                pro.CreatedBy =data.CreatedBy;
                pro.CreatedDate = data.CreatedDate;
                pro.ModifiedDate = DateTime.Now;
                pro.ModifiedBy = "";


                _context.Customers.Update(pro);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
