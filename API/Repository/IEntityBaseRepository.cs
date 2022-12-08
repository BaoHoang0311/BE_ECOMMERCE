using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IEntityBaseRepository<T> where T: class,  new()
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetQuery();

        Task DeleteAsync(string id);
    }

}
