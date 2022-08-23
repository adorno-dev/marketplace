using Marketplace.Web.Components;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        private async Task<Select> GetSelectViewComponent(string value = "")
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectItem(s.Id.ToString(), s.Name)).ToList();

            return new Select("categoryId", "Empty Category", "", selectItems);
        }

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index() => View(await categoryService.GetCategories());

        public async Task<IActionResult> Create()
        {
            ViewBag.SelectViewComponent = await GetSelectViewComponent();

            return View();
        }

        public async Task<IActionResult> Details(ushort id) => View(await categoryService.GetCategory(id));

        public async Task<IActionResult> Edit(ushort id)
        {
            ViewBag.SelectViewComponent = await GetSelectViewComponent(id.ToString());
            
            return View(await categoryService.GetCategory(id));
        }

        public async Task<IActionResult> Delete(ushort id) => View(await categoryService.GetCategory(id));
    }
}