using System.Security.AccessControl;
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

        public async Task<Guid?> CreateProduct(CreateProductRequest request)
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


        // public async Task<string[]?> GetScreenshoots(Guid id)
        // {
        //     string[]? screenshoots = null;

        //     try
        //     {
        //         screenshoots = Directory.GetFiles($"wwwroot/uploads/products/{id}")
        //                                 .Select(s => s.Replace("wwwroot", "https://localhost:5000"))
        //                                 .ToArray();
        //     }
        //     catch (IOException) {}
                                                
        //     await Task.CompletedTask;

        //     return screenshoots;
        // }

        // public async Task<string?> GetScreenshot(Guid id)
        // {
        //     string? screenshoot = null;

        //     try
        //     {
        //         screenshoot = Directory.GetFiles($"wwwroot/uploads/products/{id}")
        //                                .Select(s => s.Replace("wwwroot", "https://localhost:5000"))
        //                                .FirstOrDefault();
        //     }
        //     catch (IOException) {}

        //     await Task.CompletedTask;

        //     return screenshoot;
        // }

        public async Task SaveProductScreenshoots(Guid id, IFormFileCollection files)
        {
            string directory = $"wwwroot/uploads/products/{id}";

            if (! Directory.Exists(directory))
                  Directory.CreateDirectory(directory);
            
            foreach (var file in files)
                using (var fs = new FileStream($"{directory}/{file.FileName}", FileMode.Create))
                    await file.CopyToAsync(fs);

            await Task.CompletedTask;
        }
    }
}