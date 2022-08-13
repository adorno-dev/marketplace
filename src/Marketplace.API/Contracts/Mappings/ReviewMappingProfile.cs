using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<CreateReviewRequest, Review>().ReverseMap();
            CreateMap<UpdateReviewRequest, Review>().ReverseMap();
            CreateMap<ReviewResponse, Review>().ReverseMap();
        }
    }
}