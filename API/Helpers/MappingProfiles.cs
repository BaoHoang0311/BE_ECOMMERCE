using API.Dtos;
using API.Entites;
using AutoMapper;


namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductDtos, Product>().ReverseMap();

            CreateMap<CustomerDtos, Customer>().ReverseMap();

            CreateMap<OrderDtos, Order>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderId))
                .ForMember(d => d.OrderDetails, o => o.MapFrom(s => s.orderDetailDtos));

            CreateMap<OrderDetailDtos, OrderDetail>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ammount, o => o.MapFrom(s => s.ProductAmmount))
                .ForMember(d => d.price, o => o.MapFrom(s => s.Price));

            CreateMap<BuyOrderDtos, BuyOrder>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.BuyOrderId))
                .ForMember(d => d.BuyOrderDetails , o => o.MapFrom(s => s.BuyorderDetailDtos));

            CreateMap<BuyOrderDetailDtos, BuyOrderDetail>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ammount, o => o.MapFrom(s => s.ProductAmmount))
                .ForMember(d => d.price, o => o.MapFrom(s => s.Price));

        }
    }
}