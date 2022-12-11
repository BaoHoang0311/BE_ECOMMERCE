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

        // Fe xử lý
        Task<bool> AddOrderAsync_1(OrderDtos orderDtos);

        Task<IList<Order>> GetOrderbyOrderId(int OrderId);

        Task<bool> UpdateOrder(OrderDtos orderDtos);

        IEnumerable<Order> GetAllAsyncSearchandPaging(IEnumerable<Order> source, string searchString, int? pageNumber, int pageSize);
    }
}