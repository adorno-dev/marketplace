using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public record Link (string aspController, string aspAction, string text);

    public class LinkViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string aspController, string aspAction, string text)
        {
            await Task.CompletedTask;

            return View(new Link(aspController, aspAction, text));
        }
    }
}