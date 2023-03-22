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
        public void InsertClockInToDatabaseSQL(int employeeID);
        public void InsertClockOutToDatabaseSQL(int employeeID);
        public IEnumerable<ClockInOut> GetEmployeeClockInOutHistorySQL(int employeeID);
        public bool ValidateEmployeeAndPasswordSQL(ClockInOut employee);
        public IEnumerable<Employee> GetAllClockedInOutStaffSQL();
        public IEnumerable<PaymentMethodCategory> GetAllPaymentMethodsSQL();
        public PaymentMethodCategory GetSinglePaymentMethodSQL(int id);
        public void InsertPaymentMethodInDatabaseSQL(PaymentMethodCategory pay);
        public void DeletePaymentMethodInDatabaseSQL(PaymentMethodCategory pay);
        public void UpdatePaymentMethodInDatabaseSQL(PaymentMethodCategory pay);
        public IEnumerable<MoneyAccounting> ViewAllDebitCreditHistorySQL();
        public MoneyAccounting GetSingleDebitCreditRecordSQL(int id);
        public void InsertFundsToDatabaseSQL(MoneyAccounting funds);

    }
}
