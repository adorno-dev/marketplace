using Marketplace.Web.Components;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Route("categories")]
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

        public async Task<IActionResult> Index() => View(await categoryService.GetCategoriesPaginated());

        [Route("pages/{page:int}")]
        public async Task<IActionResult> Index(int page = 1) => View(await categoryService.GetCategoriesPaginated(page));

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var createCategoryRequest = new CreateCategoryRequest();

            ViewBag.SelectViewComponent = await GetSelectViewComponent();

            return View(createCategoryRequest);
        }

        [Route("create")]
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

        [Route("details/{id}")]
        public async Task<IActionResult> Details(ushort id) => View(await categoryService.GetCategory(id));

        [Route("edit/{id}")]
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
                updateCategoryRequest.ParentId = category.ParentId;
            }

            ViewBag.SelectViewComponent = await GetSelectViewComponent(updateCategoryRequest.ParentId.ToString());
            
            return View(updateCategoryRequest);
        }

        [Route("edit/{id}")]
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

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var category = await categoryService.GetCategory(id);

            return View(category);
        }

        [Route("confirm-delete/{id}")]
        public async Task<IActionResult> ConfirmDelete(ushort id)
        {
            var success = await categoryService.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }
    }
}