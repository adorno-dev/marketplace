using System.Security.Claims;
using Marketplace.Web.Components;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IStoreService storeService;
        private readonly IProductService productService;

        private async Task<Select> GetSelectViewComponent(string? value = "")
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectItem(s.Id.ToString(), s.Name)).ToList();

            return new Select("categoryId", "Empty Category", value ?? "", selectItems);
        }

        public ProductsController(ICategoryService categoryService, IStoreService storeService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.storeService = storeService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await productService.GetProducts());

        public async Task<IActionResult> Create()
        {
            var createProductRequest = new CreateProductRequest();
            
            var uid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var store = await storeService.GetStoreByUserId(uid);

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

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
                success = await productService.CreateProduct(request);

            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        public async Task<IActionResult> Details(Guid id) => View(await productService.GetProduct(id));

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

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
                success = await productService.UpdateProduct(request);

            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        public async Task<IActionResult> Delete(Guid id) => View(await productService.GetProduct(id));

        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var success = await productService.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
    }
}