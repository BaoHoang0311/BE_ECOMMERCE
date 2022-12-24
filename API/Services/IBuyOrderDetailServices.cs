using API.Entites;
using API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IBuyOrderDetailServices : IEntityBaseRepository<BuyOrderDetail>
    {
        Task<IEnumerable<BuyOrderDetail>> GetAllListById(int id);
    }
}
