using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<CreateStoreRequest, Store>().ReverseMap();
            CreateMap<UpdateStoreRequest, Store>().ReverseMap();
            CreateMap<Store, StoreResponse>()
                .ForMember(s => s.Categories, 
                    m => m.MapFrom(
                        c => c.Categories != null ? 
                        c.Categories.Split(" ", StringSplitOptions.TrimEntries)
                            .Select(s => ushort.Parse(s))
                            .ToArray() : null));
        }
    }
}