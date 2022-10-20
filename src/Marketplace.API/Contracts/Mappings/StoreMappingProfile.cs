using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Utils;

namespace Marketplace.API.Contracts.Mappings
{
    public sealed class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<StoreResponse, User>().ReverseMap();
            CreateMap<StorePaginatedResponse, User>().ReverseMap();
            CreateMap<StoreResponse, Store>().ReverseMap();
            CreateMap<StorePaginatedResponse, Store>().ReverseMap();
            CreateMap<CreateStoreRequest, Store>().ReverseMap();
            CreateMap<UpdateStoreRequest, Store>().ReverseMap();
            CreateMap(typeof(Pagination<StoreResponse>), typeof(Pagination<Store>)).ReverseMap();
            CreateMap(typeof(Pagination<StorePaginatedResponse>), typeof(Pagination<Store>)).ReverseMap();
        }
    }
}