using API.Data;
using API.Dtos;
using API.Entites;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IOrderRepository :IEntityBaseRepository<Order>
    {
        Task<bool> AddOrderAsync(OrderDtos orderDtos);

        Task<IList<Order>> GetOrderbyOrderId(int OrderId);

        Task<bool> UpdateOrder(OrderDtos orderDtos);
    }
}