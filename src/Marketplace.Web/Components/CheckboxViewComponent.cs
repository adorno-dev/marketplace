using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public record Checkbox (string name, string text);

    public class CheckboxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string name, string text)
        {
            await Task.CompletedTask;

            return View(new Checkbox(name, text));
        }
    }
}