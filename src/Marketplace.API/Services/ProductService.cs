using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductService(IMapper mapper, IProductRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        
        public async Task<IEnumerable<ProductResponse>?> GetProducts()
        {
            var products = await repository.GetProducts();

            return mapper.Map<IEnumerable<ProductResponse>?>(products);
        }

        public async Task<ProductResponse?> GetProduct(Guid id)
        {
            var product = await repository.GetProduct(id);

            return mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> CreateProduct(CreateProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            var response = await repository.CreateProduct(product);

            return mapper.Map<ProductResponse>(response);
        }

        public async Task<ProductResponse?> UpdateProduct(UpdateProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            var response = await repository.UpdateProduct(product);

            return mapper.Map<ProductResponse>(response);
        }

        public async Task<ProductResponse?> DeleteProduct(Guid id)
        {
            var response = await repository.DeleteProduct(id);

            return mapper.Map<ProductResponse>(response);
        }
    }
}