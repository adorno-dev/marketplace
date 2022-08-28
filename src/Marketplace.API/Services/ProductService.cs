using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;

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

        public async Task<IPagination<ProductResponse>?> GetProductsPaginated(int skip, int take, bool includeParent = false)
        {
            var products = await repository.GetProductsPaginated(skip, take, includeParent);

            return mapper.Map<Pagination<ProductResponse>?>(products);
        }

        public async Task<ProductResponse?> GetProduct(Guid id)
        {
            var product = await repository.GetProduct(id);

            return mapper.Map<ProductResponse>(product);
        }

        public async Task<bool> CreateProduct(CreateProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            return await repository.CreateProduct(product);
        }

        public async Task<bool> UpdateProduct(UpdateProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            return await repository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            return await repository.DeleteProduct(id);
        }
    }
}