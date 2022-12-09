using API.Dtos;
using API.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IBuyOrderRepository : IEntityBaseRepository<BuyOrder>
    {
        Task<bool> AddBuyOrderAsync(BuyOrderDtos orderDtos);
        Task<IList<BuyOrder>> GetBuyOrderbyOrderId(int CusId);

        Task<bool> UpdateBuyOrder(BuyOrderDtos orderDtos);
    }
}
