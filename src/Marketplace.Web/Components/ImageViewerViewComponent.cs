using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public class ImageViewerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()  
        {
            await Task.CompletedTask;

            return View();
        }
    }
}