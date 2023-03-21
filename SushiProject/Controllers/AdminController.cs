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
        public IActionResult ClockIn()
        {
            ClockInOut clockInOut = new ClockInOut();
            clockInOut.Success = true;
            return View("ClockIn", clockInOut);
        }
        public IActionResult ClockOut()
        {
            ClockInOut clockInOut = new ClockInOut();
            clockInOut.Success = true;
            return View("ClockOut", clockInOut);
        }
        public IActionResult ClockInToDatabase(ClockInOut employee)
        {
            if(ModelState.IsValid)
            {
                //Need to validate if EmployeeID entered exists - if it does not, there will be an error thrown.
                bool success = repo.ValidateEmployeeAndPasswordSQL(employee);
                if (success == true)
                {
                    repo.InsertClockInToDatabaseSQL((int)employee.EmployeeID);
                    var employeeClockInOutHistory = repo.GetEmployeeClockInOutHistorySQL((int)employee.EmployeeID);
                    employee.ClockInOutHistory = employeeClockInOutHistory;
                    return View("SuccessfullyClockedInOut", employee);
                }
                else
                {
                    employee.Success = false;
                    return View("ClockIn", employee);
                }
            }
            else
            {
                employee.Success = false;
                return View("ClockIn", employee);
            }

        }
        public IActionResult ClockOutToDatabase(ClockInOut employee)
        {
            bool success = repo.ValidateEmployeeAndPasswordSQL(employee);
            if (success == true)
            {
                repo.InsertClockOutToDatabaseSQL((int)employee.EmployeeID);
                var employeeClockInOutHistory = repo.GetEmployeeClockInOutHistorySQL((int)employee.EmployeeID);
                employee.ClockInOutHistory = employeeClockInOutHistory;
                return View("SuccessfullyClockedInOut", employee);
            }
            else
            {
                employee.Success = false;
                return View("ClockOut", employee);
            }
        }
        public IActionResult ViewClockedInOutStaff()
        {
            var staffList = repo.GetAllClockedInOutStaffSQL();
            return View("ClockedInOutStaff", staffList);
        }
        public IActionResult ViewAllPaymentMethods()
        {
            IEnumerable<PaymentMethodCategory> paymentMethodsList = repo.GetAllPaymentMethodsSQL();
            return View("ViewAllPaymentMethods", paymentMethodsList);
        }
        public IActionResult ViewSinglePaymentMethod(int id)
        {
            PaymentMethodCategory paymentMethod = repo.GetSinglePaymentMethodSQL(id);
            return View("ViewSinglePaymentMethod", paymentMethod);
        }
        public IActionResult InsertPaymentMethod()
        {
            PaymentMethodCategory paymentMethod = new PaymentMethodCategory();
            return View("InsertPaymentMethod", paymentMethod);
        }
        public IActionResult InsertPaymentMethodToDatabase(PaymentMethodCategory pay)
        {
            repo.InsertPaymentMethodInDatabaseSQL(pay);
            return RedirectToAction("ViewAllPaymentMethods");
        }
        public IActionResult UpdatePaymentMethod(int id)
        {
            PaymentMethodCategory paymentMethod = repo.GetSinglePaymentMethodSQL(id);
            return View("UpdatePaymentMethod", paymentMethod);
        }
        public IActionResult UpdatePaymentMethodInDatabase(PaymentMethodCategory pay)
        {
            repo.UpdatePaymentMethodInDatabaseSQL(pay);
            return RedirectToAction("ViewAllPaymentMethods");
        }
        public IActionResult DeletePaymentMethod(PaymentMethodCategory pay)
        {
            repo.DeletePaymentMethodInDatabaseSQL(pay);
            return RedirectToAction("ViewAllPaymentMethods");
        }
    }
}
