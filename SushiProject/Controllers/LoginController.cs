using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;
using SushiProject.Services.Business;

namespace SushiProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login(UserModel userModel)
        {
            //return $"Results: Username = {userModel.Username} PW = {userModel.Password}";
            SecurityService securityService = new SecurityService();
            bool success = securityService.Authenticate(userModel);
            
            if (success)
            {
                return View("LoginSuccess", userModel);
            }
            else
            {
                return View("LoginFailure");
            }
        }

    }
}
