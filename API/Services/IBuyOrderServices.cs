using API.Dtos;
using API.Entites;
using API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IBuyOrderServices : IEntityBaseRepository<BuyOrder>
    {
        Task<bool> AddBuyOrderAsync(BuyOrderDtos orderDtos);
        Task<bool> AddBuyOrderAsync_1(BuyOrderDtos orderDtos);
        Task<IList<BuyOrder>> GetBuyOrderbyOrderId(int CusId);

        Task<bool> UpdateBuyOrder(BuyOrderDtos orderDtos);
    }
}
