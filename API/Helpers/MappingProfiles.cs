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
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.PictureUrl))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

            CreateMap<Product,Product> ()
                    .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                    .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.PictureUrl))
                    .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                    .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                    .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<CustomerDtos, Customer>();

        }
    }
}