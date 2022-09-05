using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public record Link (string controller, string action, string text);

    public class LinkViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string controller, string action, string text)
        {
            await Task.CompletedTask;

            return View(new Link(controller, action, text));
        }
    }
}