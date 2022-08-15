using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index() => View(await categoryService.GetCategories());

        public IActionResult Create() => View();

        public async Task<IActionResult> Details(ushort id) => View(await categoryService.GetCategory(id));

        public async Task<IActionResult> Edit(ushort id) => View(await categoryService.GetCategory(id));

        public async Task<IActionResult> Delete(ushort id) => View(await categoryService.GetCategory(id));
    }
}