using API.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IBuyOrderDetailRepository : IEntityBaseRepository<BuyOrderDetail>
    {
        Task<IEnumerable<BuyOrderDetail>> GetAllListById(int id);
    }
}
