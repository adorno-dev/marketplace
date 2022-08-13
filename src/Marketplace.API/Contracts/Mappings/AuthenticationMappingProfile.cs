using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<SignUpRequest, User>()
                .ForMember(m => m.UserName, f => f.MapFrom(p => p.Username));
            
            CreateMap<string, AuthenticationResponse>()
                .ForMember(m => m.Token, f => f.MapFrom(p => p));
        }
    }
}