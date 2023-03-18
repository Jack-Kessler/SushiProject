using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository repo;

        public AdminController(IAdminRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeTaxRate()
        {
            return View("ChangeTaxRate");
        }
        public IActionResult InsertTaxRateToDatabase(TaxRate rate)
        {
            var success = repo.ChangeTaxRateSQL(rate);
            if (success == true)
            {
                return View("TaxRateChangedSuccessfully");
            }
            return View("ChangeTaxRate", rate);
        }
        public IActionResult ChangeAllYouCanEatRate()
        {
            return View("ChangeAllYouCanEatRate");
        }
        public IActionResult InsertAllYouCanEatRateToDatabase(AllYouCanEat rate)
        {
            var success1 = repo.ChangeAllYouCanEatRateAdultSQL(rate);
            var success2 = repo.ChangeAllYouCanEatRateChildSQL(rate);
            if (success1 == true && success2 == true)
            {
                return View("AllYouCanEatRateChangedSuccessfully");
            }
            return View("ChangeAllYouCanEatRate", rate);
        }
        public IActionResult ChangeCustomerLogoutPassword()
        {
            return View("ChangeCustomerLogoutPassword");
        }
        public IActionResult InsertCustomerLogoutPasswordToDatabase(CustomerLogoutPassword password)
        {
            var success = repo.ChangeCustomerLogoutPasswordSQL(password);
            if (success == true)
            {
                return View("PasswordChangedSuccessfully");
            }
            return View("ChangeCustomerLogoutPassword", password);
        }
    }
}
