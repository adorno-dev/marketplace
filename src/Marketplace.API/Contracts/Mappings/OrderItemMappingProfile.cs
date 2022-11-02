using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItemResponse, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(m => m.UserId, o => o.MapFrom(s => s.Order != null ? s.Order.UserId : Guid.Empty))
                .ForMember(m => m.ProductName, o => o.MapFrom(p => p.Product != null ? p.Product.Name : ""));

            // CreateMap<OrderItem, OrderItemResponse>().ForMember(m => m.ProductName, o => o.MapFrom(p => p.Product != null ? p.Product.Name : ""));
        }
    }
}