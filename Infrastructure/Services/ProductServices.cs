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
    public class ProductServices : IProductService
    {
        private readonly MyDbContext _context; 
        public ProductServices(MyDbContext context)
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

        public async Task<bool> AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(string id, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
