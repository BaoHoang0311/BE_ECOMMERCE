using API.Dtos;
using API.Entites;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {
        Task<bool> AddAsync(CustomerDtos entity);
        Task<bool> UpdateAsync(CustomerDtos entity);
    }
}
