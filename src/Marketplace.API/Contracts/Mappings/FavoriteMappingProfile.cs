using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Utils;

namespace Marketplace.API.Contracts.Mappings
{
    public class FavoriteMappingProfile : Profile
    {
        #pragma warning disable CS8602
        public FavoriteMappingProfile()
        {
            CreateMap<FavoriteResponse, Product>().ReverseMap();
            CreateMap<FavoriteResponse, Favorite>()
                .ForMember(m => m.Product, o => o.MapFrom(s => s))
                .ForPath(m => m.Product.Store.Name, o => o.MapFrom(s => s.Store))
                .ReverseMap();

            CreateMap(typeof(Pagination<FavoriteResponse>), typeof(Pagination<Favorite>)).ReverseMap();
        }
    }
}