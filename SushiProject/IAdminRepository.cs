using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject
{
    public interface IAdminRepository
    {
        public bool ChangeCustomerLogoutPasswordSQL(CustomerLogoutPassword password);
        public void UpdateCustomerLogoutPasswordInDatabaseSQl(CustomerLogoutPassword password);
        public bool ChangeAllYouCanEatRateSQL(AllYouCanEat eatRate);
        public void UpdateAllYouCanEatRateInDatabaseSQl(AllYouCanEat eatRate);
        public bool ChangeTaxRateSQL(TaxRate taxRate);
        public void UpdateTaxRateInDatabaseSQl(TaxRate taxRate);
        public void InsertClockInToDatabase(int employeeID);
        public void InsertClockOutToDatabase(int employeeID);
        public IEnumerable<ClockInOut> GetEmployeeClockInOutHistory(int employeeID);
        public bool ValidateEmployeeAndPasswordSQL(ClockInOut employee);
    }
}
