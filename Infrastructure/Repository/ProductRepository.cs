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
    public class ProductRepository : IProductService
    {
        private readonly MyDbContext _context; 
        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var data = await _context.products.ToListAsync();
            return data;
        }
        public async Task<Product> GetByIdAsync(string id)
        {
            var data = await _context.products.FindAsync(id);
            return data;
        }
        public Task<Product> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        // create
        public async Task AddAsync(Product entity)
        {
            _context.products.Add(entity);

        }
        //delete
        public async Task DeleteAsync(string id)
        {
            var data =await GetByIdAsync(id);
            if (data != null)
            {
                _context.products.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
        //put
        public async Task UpdateAsync(string id, Product entity)
        {
            if (GetByIdAsync(id) != null)
            {
                _context.products.Add(entity);
                await _context.SaveChangesAsync();
            }
        }


    }
}
