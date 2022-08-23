using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public record SelectItem (string id, string? text);

    public record Select (string name, string text, string value, IList<SelectItem>? items);

    public class SelectViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Select model)
        {
            await Task.CompletedTask;

            return View(model);
        }
    }
}