using System.Security.Claims;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser<Guid>> signInManager;
        private readonly IAccountService accountService;
        private readonly ITokenService tokenService;

        public AccountController(SignInManager<IdentityUser<Guid>> signInManager, IAccountService accountService, ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.accountService = accountService;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult SignIn() => View(new SignInRequest());

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var response = await accountService.SignIn(request);

            if (response?.Token is not null)
            {
                var claimsPrincipal = tokenService.GetClaimsPrincipalFromExpiredToken(response.Token);

                var identifier = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (identifier is not null)
                {
                    var user = new IdentityUser<Guid> 
                    { 
                        Id = Guid.Parse(identifier), 
                        Email = request.Email, 
                        UserName = request.Email,
                        SecurityStamp = Guid.NewGuid().ToString() 
                    };

                    await signInManager.SignInWithClaimsAsync(user, request.Remember, claimsPrincipal?.Claims);

                    return RedirectToAction("index", "home");
                }
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult SignUp() => View(new SignUpRequest());

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await accountService.SignUp(request);

                return RedirectToAction("index", "home");
            }

            return View(request);
        }

        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();
    }
}