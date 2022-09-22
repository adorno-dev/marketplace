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

            return mapper.Map<IEnumerable<StoreResponse>?>(stores);
        }

        public async Task<StoreResponse?> GetStore(Guid id)
        {
            var store = await repository.GetStore(id);

            return mapper.Map<StoreResponse?>(store);
        }

        public async Task<StoreResponse?> GetStoreByUserId(Guid userId)
        {
            var store = await repository.GetStoreByUserId(userId);

            return mapper.Map<StoreResponse?>(store);
        }

        public async Task<bool> CreateStore(CreateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);
            
            return await repository.CreateStore(store);
        }

        public async Task<bool> UpdateStore(UpdateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);

            return await repository.UpdateStore(store);
        }

        public async Task<bool> DeleteStore(Guid id)
        {
            return await repository.DeleteStore(id);
        }
    }
}