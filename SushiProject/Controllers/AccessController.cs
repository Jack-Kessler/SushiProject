using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SushiProject.Models;
//using SushiProject.Services.Business;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SushiProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccessRepository repo;

        public AccessController (IAccessRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Employee employeeToLogin)
        {
            //SecurityService securityService = new SecurityService();
            //bool success = securityService.Authenticate(modelLogin); //Authenticates user

            bool success = repo.UserPassAuthenticate(employeeToLogin);

            if (success == true)
            {
                List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, employeeToLogin.UserName),
                    };

                if (employeeToLogin.Role == "Server")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Server");
                    claims.Add(newClaim);
                }
                else if (employeeToLogin.Role == "Chef")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Chef");
                    claims.Add(newClaim);
                }
                else if (employeeToLogin.Role == "Manager")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Manager");
                    claims.Add(newClaim);
                }
                else if(employeeToLogin.Role == "Owner")
                {
                    var newClaim = new Claim(ClaimTypes.Role, "Owner");
                    claims.Add(newClaim);
                }
                else
                {
                    ViewData["ValidateMessage"] = "Role Not Found";
                    return View(employeeToLogin);
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = employeeToLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt");
                ViewData["ValidateMessage"] = "Error: The username you entered does not exist or the password was incorrect. Please try again.";
                return View(employeeToLogin);
                //return View("LoginFailure");
            }

            //if (modelLogin.UserName == null) { }
            //return View();
        }
    }
}
