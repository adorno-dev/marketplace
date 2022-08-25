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

            return new Select("categoryId", "Empty Category", value, selectItems);
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
            var category = await categoryService.GetCategory(id);

            if (category is null)
                return NotFound();

            var parentId = category.Parent is not null ? 
                category.Parent.Id.ToString() :
                string.Empty;

            ViewBag.SelectViewComponent = await GetSelectViewComponent(parentId);
            
            return View(category);
        }

        public async Task<IActionResult> Delete(ushort id)
        {
            var category = await categoryService.GetCategory(id);

            return View(category);
        }
    }
}