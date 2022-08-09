using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

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

        public async Task<CategoryResponse?> GetCategory(ushort id, bool includeParent = false)
        {
            var category = await repository.GetCategory(id, includeParent);

            return mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse?> CreateCategory(CreateCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);

            if (request.ParentId is not null)
                category.Parent = await repository.GetCategory(request.ParentId.Value);

            var response = await repository.CreateCategory(category);

            return mapper.Map<CategoryResponse>(response);
        }

        public async Task<CategoryResponse?> UpdateCategory(UpdateCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);

            if (request.ParentId is not null)
                category.Parent = await repository.GetCategory(request.ParentId.Value);

            var response = await repository.UpdateCategory(category);

            return mapper.Map<CategoryResponse>(response);
        }

        public async Task<CategoryResponse?> DeleteCategory(ushort id)
        {
            var response = await repository.DeleteCategory(id);

            return mapper.Map<CategoryResponse>(response);
        }
    }
}