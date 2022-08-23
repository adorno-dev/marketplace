using Marketplace.Web.Components;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        private async Task<Select> GetSelectViewComponent()
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectItem(s.Id.ToString(), s.Name)).ToList();

            return new Select("categoryId", "Empty Category", "", selectItems);
        }

        public ProductsController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await productService.GetProducts());

        public async Task<IActionResult> Create()
        {
            ViewBag.SelectViewComponent = await GetSelectViewComponent();

            return View();
        }

        public async Task<IActionResult> Details(Guid id) => View(await productService.GetProduct(id));

        public async Task<IActionResult> Edit(Guid id) => View(await productService.GetProduct(id));

        public async Task<IActionResult> Delete(Guid id) => View(await productService.GetProduct(id));
    }
}