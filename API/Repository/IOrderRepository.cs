using API.Data;
using API.Dtos;
using API.Entites;
using API.Services;
using API.Specifications;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderDtos orderDtos);
    }
}