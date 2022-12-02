using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Services
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityID, new()
    {
        private readonly MyDbContext _context;
        public EntityBaseRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var check = await _context.Set<T>().FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (check != null)
            {
                return false;
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(string id)
        {
            var data = GetByIdAsync(id);
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }

        public DbSet<T> Get()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var data = await _context.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id.ToString() == id);
            return data;
        }

        public Task<Product> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        Task IEntityBaseRepository<T>.AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task IEntityBaseRepository<T>.UpdateAsync(string id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
