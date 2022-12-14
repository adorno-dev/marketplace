using System.Security.Claims;
using Marketplace.Web.Components;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Route("products")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IStoreService storeService;
        private readonly IProductService productService;
        private readonly IFavoriteService favoriteService;

        private async Task<Select> GetSelectViewComponent(string? value = "")
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectItem(s.Id.ToString(), s.Name)).ToList();

            return new Select("categoryId", "Empty Category", value ?? "", selectItems);
        }

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public ProductsController(ICategoryService categoryService, IStoreService storeService, IProductService productService, IFavoriteService favoriteService)
        {
            this.categoryService = categoryService;
            this.storeService = storeService;
            this.productService = productService;
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductsPaginated();

            if (products is null)
                products = new Pagination<Product>() { Items = Array.Empty<Product>() };

            return View(products);
        }

        [Route("pages/{page:int}")]
        public async Task<IActionResult> Index(int page = 1) => View(await productService.GetProductsPaginated(page));

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var createProductRequest = new CreateProductRequest();
            
            var store = await storeService.GetStoreByUserId(UserId);

            if (store is null)
                // Store is required to create products.
                RedirectToAction("index", "stores");
            else
            {
                createProductRequest.StoreId = store.Id;
            }
            
            ViewBag.SelectViewComponent = await GetSelectViewComponent();

            return View(createProductRequest);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                var info = Directory.CreateDirectory($"wwwroot/uploads/products/{request.Id}".Replace('/', Path.DirectorySeparatorChar));

                if (request.Images is not null)
                    foreach (var item in request.Images)
                    {
                        using (var fs = new FileStream(info.FullName + Path.DirectorySeparatorChar + item.FileName, FileMode.Create))
                            await item.CopyToAsync(fs);
                    }

                success = await productService.CreateProduct(request);
            }

            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(Guid id) => View(await productService.GetProduct(id));

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var updateProductRequest = new UpdateProductRequest();

            var product = await productService.GetProduct(id);

            if (product is null)
                return NotFound();
            else
            {
                updateProductRequest.Id = id;
                updateProductRequest.StoreId = product.Store?.Id;
                updateProductRequest.CategoryId = product.Category?.Id;
                updateProductRequest.Name = product.Name;
                updateProductRequest.Description = product.Description;
                updateProductRequest.Stock = product.Stock;
                updateProductRequest.Price = product.Price;
            }
            
            ViewBag.SelectViewComponent = await GetSelectViewComponent(updateProductRequest.CategoryId.ToString());

            return View(updateProductRequest);
        }

        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                var info = Directory.CreateDirectory($"wwwroot/uploads/products/{request.Id}".Replace('/', Path.DirectorySeparatorChar));

                if (request.Images is not null)
                    foreach (var item in request.Images)
                    {
                        using (var fs = new FileStream(info.FullName + Path.DirectorySeparatorChar + item.FileName, FileMode.Create))
                            await item.CopyToAsync(fs);
                    }

                success = await productService.UpdateProduct(request);
            }

            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id) => View(await productService.GetProduct(id));

        [Route("confirm-delete/{id}")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var success = await productService.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }

        // 

        [AllowAnonymous]
        [Route("view/{id}")]
        [ActionName("view")]
        public async Task<IActionResult> ViewProduct(Guid id)
        {
            var product = await productService.GetProduct(id);

            return View(product);
        }

        // 

        [Route("favorite/{productId}")]
        [ActionName("favorite")]
        public async Task<IActionResult> Favorite(Guid productId)
        {
            await favoriteService.Favorite(productId);

            return RedirectToAction("view", new { id = productId });
        }

        [Route("unfavorite/{productId}")]
        [ActionName("unfavorite")]
        public async Task<IActionResult> Unfavorite(Guid productId)
        {
            await favoriteService.UnFavorite(productId);

            return RedirectToAction("view", new { id = productId });
        }
    }
}