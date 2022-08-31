using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Components
{
    public class ImageBrowserViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string path)
        {
            var root = $"{path.Replace('/', Path.DirectorySeparatorChar)}";

            await Task.CompletedTask;

            return Directory.Exists(root) ?
                View(Directory.GetFiles(root).Select(s => s.Replace("wwwroot", "")).ToArray()):
                View(Array.Empty<string>());
        }
    }
}