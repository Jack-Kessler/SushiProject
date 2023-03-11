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
                    };

                if (modelLogin.Role == "Server")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Server");
                    claims.Add(newClaim);
                }
                else if (modelLogin.Role == "Chef")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Chef");
                    claims.Add(newClaim);
                }
                else if (modelLogin.Role == "Manager")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Manager");
                    claims.Add(newClaim);
                }
                else if(modelLogin.Role == "Owner")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Owner");
                    claims.Add(newClaim);
                }
                else
                {
                    ViewData["ValidateMessage"] = "Role Not Found";
                    return View(modelLogin);
                }

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
                ViewData["ValidateMessage"] = "User does not exist or Password is incorrect";
                return View(modelLogin);
                //return View("LoginFailure");
            }

            //if (modelLogin.UserName == null) { }
            //return View();
        }
    }
}
