using System.Security.Claims;
using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthenticationController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            if (ModelState.IsValid)
            {
                var claims = new List<Claim>();
                
                var user = mapper.Map<User>(request);

                var result = await userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));

                    result = await userManager.AddClaimsAsync(user, claims);

                    return result.Succeeded ?
                        Ok():
                        BadRequest(result.Errors);
                }
            }
            
            return BadRequest();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(request.Email);

                if (user is not null && await userManager.CheckPasswordAsync(user, request.Password))
                {
                    tokenService.GenerateToken(user, out string token);

                    var credentials = new AuthenticationResponse 
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = token
                    };

                    return string.IsNullOrEmpty(token) ?
                        BadRequest():
                        Ok(credentials);
                        // Ok(mapper.Map<AuthenticationResponse>(token));
                }
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(request.Email);

                // TODO: implementar rotina de recuperacao de senha

                return user is not null ?
                    Ok():
                    BadRequest();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("whoami")]
        public async Task<IActionResult> WhoAmI()
        {
            tokenService.GetUserInfo(HttpContext, out string? userId, out string? userName, out string? email);

            await Task.CompletedTask;

            if (userId is null)
                return BadRequest("Unknown User ID");

            return Ok(new UserResponse
            {
                Id = Guid.Parse(userId),
                Username = userName,
                Email = email
            });
        }
    }
}