using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetCategories();

            return View(categories);
        }
    }
}