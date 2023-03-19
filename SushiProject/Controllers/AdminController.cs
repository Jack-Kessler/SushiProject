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
            var rate = new TaxRate();
            rate.Success = true;
            return View("ChangeTaxRate", rate);
        }
        public IActionResult InsertTaxRateToDatabase(TaxRate rate)
        {
            var success = repo.ChangeTaxRateSQL(rate);
            if (success == true)
            {
                repo.UpdateTaxRateInDatabaseSQl(rate);
                return View("TaxRateChangedSuccessfully");
            }
            rate.Success = false;
            return View("ChangeTaxRate", rate);
        }
        public IActionResult ChangeAllYouCanEatRateAdult()
        {
            var rate = new AllYouCanEat();
            rate.Success = true;
            rate.Adult = true;
            return View("ChangeAllYouCanEatRate", rate);
        }
        public IActionResult ChangeAllYouCanEatRateChild()
        {
            var rate = new AllYouCanEat();
            rate.Success = true;
            rate.Adult = false;
            return View("ChangeAllYouCanEatRate", rate);
        }
        public IActionResult InsertAllYouCanEatRateToDatabase(AllYouCanEat rate)
        {
            var success = repo.ChangeAllYouCanEatRateSQL(rate);
            if (success == true)
            {
                repo.UpdateAllYouCanEatRateInDatabaseSQl(rate);
                return View("AllYouCanEatRateChangedSuccessfully");
            }
            rate.Success = false;
            return View("ChangeAllYouCanEatRate", rate);
        }
        public IActionResult ChangeCustomerLogoutPassword()
        {
            var pass = new CustomerLogoutPassword();
            pass.Success = true;
            return View("ChangeCustomerLogoutPassword", pass);
        }
        public IActionResult InsertCustomerLogoutPasswordToDatabase(CustomerLogoutPassword password)
        {
            var success = repo.ChangeCustomerLogoutPasswordSQL(password);
            if (success == true)
            {
                repo.UpdateCustomerLogoutPasswordInDatabaseSQl(password);
                return View("PasswordChangedSuccessfully");
            }
            password.Success = false;
            return View("ChangeCustomerLogoutPassword", password);
        }
    }
}
