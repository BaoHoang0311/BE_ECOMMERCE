using API.Data;
using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IOrderServices : IEntityBaseRepository<Order>
    {
        // Be xử lý
        Task<bool> AddOrderAsync(OrderDtos orderDtos);

        // Fe xử lý
        Task<bool> AddOrderAsync_1(OrderDtos orderDtos);

        Task<IList<Order>> GetOrderbyOrderId(int OrderId);

        Task<bool> UpdateOrder(OrderDtos orderDtos);

    }
}