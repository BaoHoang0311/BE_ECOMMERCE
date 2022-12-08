using API.Data;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace API.Services
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityID , new()
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper; 
        public EntityBaseRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var data = await _context.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            return data;
        }
        public IQueryable<T> GetQuery()
        {
            var data =  _context.Set<T>().AsQueryable();
            return data;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
            if(data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

    }
}
