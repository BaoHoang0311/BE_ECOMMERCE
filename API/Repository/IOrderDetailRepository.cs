using API.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IOrderDetailRepository : IEntityBaseRepository<OrderDetail> 
    {
        // GetAll List Order and BuyOrder
        Task<IEnumerable<OrderDetail>> GetAllListById(int id);
    }
}
