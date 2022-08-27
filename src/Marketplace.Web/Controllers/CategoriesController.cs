using Marketplace.Web.Components;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        private async Task<Select> GetSelectViewComponent(string? value = "")
        {
            var categories = await categoryService.GetCategories();

            var selectItems = categories?.Select(s => new SelectItem(s.Id.ToString(), s.Name)).ToList();

            return new Select("parentId", "Empty Category", value ?? "", selectItems);
        }

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index() => View(await categoryService.GetCategories());

        public async Task<IActionResult> Create()
        {
            var createCategoryRequest = new CreateCategoryRequest();

            ViewBag.SelectViewComponent = await GetSelectViewComponent();

            return View(createCategoryRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
                success = await categoryService.CreateCategory(request);
            
            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        public async Task<IActionResult> Details(ushort id) => View(await categoryService.GetCategory(id));

        public async Task<IActionResult> Edit(ushort id)
        {
            var updateCategoryRequest = new UpdateCategoryRequest();

            var category = await categoryService.GetCategory(id);

            if (category is null)
                return NotFound();
            else
            {
                updateCategoryRequest.Id = id;
                updateCategoryRequest.Name = category.Name;
                updateCategoryRequest.ParentId = category.Parent?.Id;
            }

            ViewBag.SelectViewComponent = await GetSelectViewComponent(updateCategoryRequest.ParentId.ToString());
            
            return View(updateCategoryRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryRequest request)
        {
            bool success = false;

            if (ModelState.IsValid)
                success = await categoryService.UpdateCategory(request);

            return success ?
                RedirectToAction(nameof(Index)):
                View(request);
        }

        public async Task<IActionResult> Delete(ushort id)
        {
            var category = await categoryService.GetCategory(id);

            return View(category);
        }

        public async Task<IActionResult> ConfirmDelete(ushort id)
        {
            var success = await categoryService.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }
    }
}