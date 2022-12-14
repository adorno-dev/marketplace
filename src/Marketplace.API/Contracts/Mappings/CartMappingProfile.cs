using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartPaginatedResponse, Cart>().ReverseMap();
            CreateMap<CartResponse, Cart>().ReverseMap();
            CreateMap<CartItemResponse, CartItem>().ReverseMap();
        }
    }
}