using API.Dtos;
using API.Entites;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {
        Task AddAsync(CustomerDtos entity);
        Task UpdateAsync(string id, CustomerDtos entity);
    }
}
