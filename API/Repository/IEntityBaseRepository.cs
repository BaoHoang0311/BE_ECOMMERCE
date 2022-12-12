using API.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IEntityBaseRepository<T> where T: class,  new()
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> GetQuery();

        Task DeleteAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        // Sort
        Task<IEnumerable<T>> GetAllAsyncSortById(string sortBy);
        // Sort with table
        Task<IEnumerable<T>> GetAllAsyncSortById( string sortBy, params Expression<Func<T, object>>[] includeProperties );


    }

}
