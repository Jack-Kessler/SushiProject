using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using SushiProject.Models;
using SushiProject.Services.Business;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SushiProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMenuItemRepository repo;
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Employee modelLogin)
        {
            SecurityService securityService = new SecurityService();
            bool success = securityService.Authenticate(modelLogin); //Authenticates user

            if (success == true)
            {
                
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                };

                
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt");
                ViewData["ValidateMessage"] = "User Not Found";
                return View(modelLogin);
                //return View("LoginFailure");
            }


            //if (modelLogin.UserName == null) { }
            //return View();
        }
    }
}
