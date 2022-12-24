using API.Entites;
using API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IOrderDetailServices : IEntityBaseRepository<OrderDetail>
    {
        // GetAll List Order and BuyOrder
        Task<IEnumerable<OrderDetail>> GetAllListById(int id);
    }
}
