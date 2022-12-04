using Core.Entites;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEntityBaseRepository<T> where T: class,  new()
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByNameAsync(string name);


        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);

        // Include Entity with condition
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        // Include Entity
        Task<List<T>> ListAsync(ISpecification<T> spec);
    }

}
