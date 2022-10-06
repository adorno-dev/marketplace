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
    public sealed class StoreService : IStoreService
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

        public async Task<IPagination<StoreResponse>?> GetStoresPaginated(int skip, int take)
        {
            var stores = await repository.GetStoresPaginated(skip, take);

            return mapper.Map<Pagination<StoreResponse>?>(stores);
        }

        public async Task<StoreResponse?> GetStore(Guid id)
        {
            var store = await repository.GetStore(id);

            return mapper.Map<StoreResponse?>(store);
        }

        public async Task<StoreResponse?> GetStoreByUserId(Guid userId)
        {
            var store = await repository.GetStoreByUserId(userId);

            var response = mapper.Map<StoreResponse?>(store);

            if (response is not null)
            {
                response.Logo = GetLogo(userId);
                response.Banner = GetBanner(userId);
            }
            
            return response;
        }

        public async Task<Guid?> GetStoreIdByUserId(Guid userId)
        {
            return await repository.GetStoreIdByUserId(userId);
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

        public string? GetLogo(Guid id) => $"https://localhost:5000/uploads/stores/{id}/logo.jpg";

        public string? GetBanner(Guid id) => $"https://localhost:5000/uploads/stores/{id}/banner.jpg";

        public async Task<bool> SaveStoreImages(Guid id, IFormFile? logo, IFormFile? banner)
        {
            string directory = $"wwwroot/uploads/stores/{id}";

            if (logo == null || banner == null)
                return false;
            
            if (! Directory.Exists(directory))
                  Directory.CreateDirectory(directory);
            
            using (var fs = new FileStream($"{directory}/{logo.FileName}", FileMode.Create))
                await logo.CopyToAsync(fs);
            
            using (var fs = new FileStream($"{directory}/{banner.FileName}", FileMode.Create))
                await banner.CopyToAsync(fs);
            
            return true;
        }
    }
}