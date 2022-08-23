using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;

            return View();
        }
    }
}