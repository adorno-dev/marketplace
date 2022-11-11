using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;
using System.Linq;

namespace Marketplace.API.Services
{
    public sealed class StoreService : IStoreService
    {
        private readonly IMapper mapper;
        private readonly IStoreRepository repository;
        private string storeImageFiles = "";
        private string storeProductFiles = "";

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

        public async Task<StorePaginatedResponse?> GetStore(Guid id, int skip, int take)
        {
            var store = await repository.GetStore(id, skip, take);

            var response = mapper.Map<StorePaginatedResponse?>(store);

            if (store is not null && response is not null)
            {
                response.Logo = store.GetLogo();
                response.Banner = store.GetBanner();
            }
            
            return response;
        }

        public async Task<StoreResponse?> GetStoreByUserId(Guid userId)
        {
            var store = await repository.GetStoreByUserId(userId);

            var response = mapper.Map<StoreResponse?>(store);

            if (store is not null && response is not null)
            {
                response.Logo = store.GetLogo();
                response.Banner = store.GetBanner();
            }
            
            return response;
        }

        public async Task<Guid?> GetStoreIdByUserId(Guid userId)
        {
            return await repository.GetStoreIdByUserId(userId);
        }

        public async Task<Guid?> CreateStore(CreateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);

            store.Joined = DateTime.UtcNow;
            
            return await repository.CreateStore(store);
        }

        public async Task<bool> UpdateStore(UpdateStoreRequest request)
        {
            var store = mapper.Map<Store>(request);

            return await repository.UpdateStore(store);
        }

        public async Task<bool> DeleteStore(Guid id)
        {
            var result = await repository.DeleteStore(id);

            // TODO: Separate this file management on delete store

            storeImageFiles = $"wwwroot/uploads/stores/{id}";            

            // if (result is not null && Directory.Exists(storeImageFiles))
            //     Directory.Delete(storeImageFiles, true);

            if (result is not null)
            {
                if (Directory.Exists(storeImageFiles))
                    Directory.Delete(storeImageFiles, true);
                
                if (result.Items != null)
                    foreach (var item in result.Items)
                    {
                        storeProductFiles = $"wwwroot/uploads/products/{item.Id}";

                        if (Directory.Exists(storeProductFiles))
                            Directory.Delete($"{storeProductFiles}", true);
                    }
            }


            return result is not null;
        }

        public async Task<bool> SaveStoreImages(Guid id, IFormFile? logo, IFormFile? banner)
        {
            storeImageFiles = $"wwwroot/uploads/stores/{id}";

            if (logo == null || banner == null)
                return false;
            
            if (! Directory.Exists(storeImageFiles))
                  Directory.CreateDirectory(storeImageFiles);
            
            using (var fs = new FileStream($"{storeImageFiles}/{logo.FileName}", FileMode.Create))
                await logo.CopyToAsync(fs);
            
            using (var fs = new FileStream($"{storeImageFiles}/{banner.FileName}", FileMode.Create))
                await banner.CopyToAsync(fs);
            
            return true;
        }
    }
}