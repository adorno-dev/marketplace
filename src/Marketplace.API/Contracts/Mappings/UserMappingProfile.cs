using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserResponse, User>().ReverseMap();
        }  
    }
}