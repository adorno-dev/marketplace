using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}