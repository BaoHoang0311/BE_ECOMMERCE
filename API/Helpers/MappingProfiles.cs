using API.Dtos;
using API.Entites;
using AutoMapper;


namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductDtos,Product>()
                .ForMember(d=>d.Id,o=>o.MapFrom(s=>s.Id))
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount));

            CreateMap<CustomerDtos, Customer>();

            CreateMap<OrderDtos, Order>()
                .ForMember(d=>d.Id , o=> o.MapFrom(s=>s.OrderId))
                .ForMember(d => d.OrderNo, o => o.MapFrom(s => s.OrderNo))
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId));

            CreateMap<OrderDetailDtos, OrderDetail>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ammount, o => o.MapFrom(s => s.ProductAmmount))
                .ForMember(d => d.price, o => o.MapFrom(s => s.Price));
                

        }
    }
}