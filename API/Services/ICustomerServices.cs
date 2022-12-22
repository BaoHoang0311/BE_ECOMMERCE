using API.Dtos;
using API.Entites;
using API.Repository;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICustomerServices : IEntityBaseRepository<Customer>
    {
    }
}
