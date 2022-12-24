using API.Data;
using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace API.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityID, new()
    {
        private readonly MyDbContext _context;
        public EntityBaseRepository(MyDbContext context)
        {
            _context = context;
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
            var data = _context.Set<T>().AsQueryable();
            return data;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(T entity)
        {

            entity.CreatedBy = "";
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = "";

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {

            entity.ModifiedDate = DateTime.Now;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<PaggingInfo<T>> GetAllAsyncSortByIdAndPaging(string sortBy, int? pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var list = query.OrderBy(m => m.Id);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Id":
                        list = list.OrderByDescending(n => n.Id);
                        break;
                    default:
                        break;
                }
            }
            //int pageSize = 5;
            var res = await Pagging<T>.Create(list.AsQueryable(), pageNumber ?? 1, pageSize);
            return res;
        }

    }
}
