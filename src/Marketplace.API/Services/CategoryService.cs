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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository repository;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IEnumerable<CategoryResponse>?> GetCategories(bool includeParent = false)
        {
            var categories = await repository.GetCategories(includeParent);

            return mapper.Map<IEnumerable<CategoryResponse>?>(categories);
        }

        public async Task<IPagination<CategoryResponse>?> GetCategoriesPaginated(int skip, int take, bool includeParent = false)
        {
            var categories = await repository.GetCategoriesPaginated(skip, take, includeParent);

            return mapper.Map<Pagination<CategoryResponse>?>(categories);
        }

        public async Task<CategoryResponse?> GetCategory(ushort id, bool includeParent = false)
        {
            var category = await repository.GetCategory(id, includeParent);

            return mapper.Map<CategoryResponse>(category);
        }

        public async Task<bool> CreateCategory(CreateCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);

            if (request.ParentId is not null)
                category.Parent = await repository.GetCategory(request.ParentId.Value);

            return await repository.CreateCategory(category);
        }

        public async Task<bool> UpdateCategory(UpdateCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);

            if (request.ParentId is not null)
                category.Parent = await repository.GetCategory(request.ParentId.Value);

            return await repository.UpdateCategory(category);
        }

        public async Task<bool> DeleteCategory(ushort id)
        {
            return await repository.DeleteCategory(id);
        }
    }
}