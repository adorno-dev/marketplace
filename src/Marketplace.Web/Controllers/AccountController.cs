using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpGet]
        public IActionResult ForgotPassword() => View();
    }
}