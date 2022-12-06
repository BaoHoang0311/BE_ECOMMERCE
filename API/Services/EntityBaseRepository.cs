using API.Data;
using API.Repository;
using API.Specifications;
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

        public async Task<T> GetByIdAsync(string id)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            return data;
        }
        public async Task<T> GetByNameAsync(string name)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.FullName == name);
            return data;
        }

        public async Task AddAsync(T entity)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if(data == null)
            {
                data.CreatedDate = DateTime.Now;
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(string id, T entity)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id  == entity.Id);
            if(data != null)
            {
                entity.ModifiedDate = DateTime.Now;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            var data = await GetByIdAsync(id);
            if(data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
