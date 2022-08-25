using Marketplace.Web.Components;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private readonly ICategoryService categoryService;

        private async Task<SelectCheckbox> GetSelectCheckboxViewComponent()
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectCheckboxItem(s.Id.ToString(), s.Name)).ToList();

            return new SelectCheckbox("categoryId", "Categories", "", selectItems);
        }

        public StoresController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SelectCheckboxViewComponent = await GetSelectCheckboxViewComponent();

            return View();
        }
    }
}