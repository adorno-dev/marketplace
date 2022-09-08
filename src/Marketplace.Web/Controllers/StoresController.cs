using System.Security.Claims;
using Marketplace.Web.Components;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IStoreService storeService;

        private async Task<SelectCheckbox> GetSelectCheckboxViewComponent(string[] values)
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectCheckboxItem(s.Id.ToString(), s.Name)).ToList();

            return new SelectCheckbox("categories", "Categories", values, selectItems);
        }

        public StoresController(ICategoryService categoryService, IStoreService storeService)
        {
            this.categoryService = categoryService;
            this.storeService = storeService;
        }

        public async Task<IActionResult> Index()
        {
            var saveStoreRequest = new SaveStoreRequest();

            var uid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var store = await storeService.GetStoreByUserId(uid);

            if (store is null)
            {
                saveStoreRequest.UserId = uid;
            }
            else
            {
                saveStoreRequest.Id = store.Id;
                saveStoreRequest.UserId = uid;
                saveStoreRequest.Name = store.Name;
                saveStoreRequest.Categories = store.Categories;
            }

            ViewBag.SelectCheckboxViewComponent = await GetSelectCheckboxViewComponent(
                saveStoreRequest.Categories != null ?
                    saveStoreRequest.Categories.Select(s => s.ToString()).ToArray():
                    Array.Empty<string>()
            );

            return View(saveStoreRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SaveStoreRequest request)
        {
            var result = await storeService.Save(request);
            
            return result ?
                RedirectToAction("index", "stores"):
                RedirectToAction(nameof(Index));
        }
    }
}