using System.Security.Claims;
using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Utils;

namespace Marketplace.API.Contracts.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();
            CreateMap(typeof(Pagination<ProductResponse>), typeof(Pagination<Product>)).ReverseMap();
        }
    }
}