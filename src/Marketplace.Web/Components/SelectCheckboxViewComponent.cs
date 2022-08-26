using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public record SelectCheckboxItem (string id, string? text);

    public record SelectCheckbox (string name, string text, string[] values, IList<SelectCheckboxItem>? items);

    public class SelectCheckboxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SelectCheckbox model)
        {
            await Task.CompletedTask;

            return View(model);
        }
    }
}