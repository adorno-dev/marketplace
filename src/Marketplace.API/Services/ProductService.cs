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
        
        public async Task<IEnumerable<ProductResponse>?> GetProducts(Guid userId)
        {
            var products = await repository.GetProducts(userId);

            return mapper.Map<IEnumerable<ProductResponse>?>(products);
        }

        public async Task<IPagination<ProductResponse>?> GetProductsPaginated(Guid userId, int skip, int take, bool includeParent = false)
        {
            var products = await repository.GetProductsPaginated(userId, skip, take, includeParent);

            return mapper.Map<Pagination<ProductResponse>?>(products);
        }

        public async Task<ProductResponse?> GetProduct(Guid userId, Guid id)
        {
            var product = await repository.GetProduct(userId, id);

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


        public async Task<string[]> GetScreenshoots(Guid id)
        {
            var screenshoots = Directory.GetFiles($"Assets/uploads/products/{id}");

            await Task.CompletedTask;

            return screenshoots;
        }
    }
}