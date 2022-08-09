using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMapper mapper;
        private readonly IStoreRepository repository;

        public StoreService(IMapper mapper, IStoreRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IEnumerable<StoreResponse>?> GetStores()
        {
            var stores = await repository.GetStores();

            var x = mapper.Map<IEnumerable<StoreResponse>?>(stores);

            return mapper.Map<IEnumerable<StoreResponse>?>(stores);
        }

        public async Task<StoreResponse?> GetStore(ushort id)
        {
            var store = await repository.GetStore(id);

            if (store?.Categories is not null)
            {
                var categoryIds = store.Categories.Split(" ").Select(s => ushort.Parse(s)).ToList();
            }

            return mapper.Map<StoreResponse?>(store);
        }

        public async Task<StoreResponse?> CreateStore(CreateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);

            if (request.Categories is not null && request.Categories.Any())
                store.Categories = string.Join(" ", request.Categories);
            
            var response = await repository.CreateStore(store);

            return mapper.Map<StoreResponse>(response);
        }

        public async Task<StoreResponse?> UpdateStore(UpdateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);

            if (request.Categories is not null && request.Categories.Any())
                store.Categories = string.Join(" ", request.Categories);
            
            var response = await repository.UpdateStore(store);

            return mapper.Map<StoreResponse>(response);
        }

        public async Task<StoreResponse?> DeleteStore(ushort id)
        {
            var response = await repository.DeleteStore(id);

            return mapper.Map<StoreResponse>(response);
        }
    }
}