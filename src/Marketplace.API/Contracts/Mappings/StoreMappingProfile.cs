using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<CreateStoreRequest, Store>().ReverseMap();
            CreateMap<UpdateStoreRequest, Store>().ReverseMap();
        }
    }
}