using Dapper;
using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnection _conn;

        public AdminRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public bool ChangeCustomerLogoutPasswordSQL(CustomerLogoutPassword password)
        {
            var currentPass = _conn.QuerySingle<CustomerLogoutPassword>("SELECT * FROM CUSTOMER_LOGOUT_PASSWORD WHERE CUSTOMERLOGOUTPASSWORDID = 1");
            if (password.CurrentPassword == currentPass.CurrentPassword && password.NewPassword == currentPass.ConfirmPassword)
            {
                return true;
            }
            return false;
        }
        public bool ChangeAllYouCanEatRateAdultSQL(AllYouCanEat eatRate)
        {
            var currentAdultRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 1");
            if (eatRate.AllYouCanEatRate == currentAdultRate.AllYouCanEatRate && eatRate.NewAllYouCanEatRate == eatRate.ConfirmAllYouCanEatRate)
            {
                return true;
            }
            return false;
        }
        public bool ChangeAllYouCanEatRateChildSQL(AllYouCanEat eatRate)
        {
            var currentChildRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 2");
            if (eatRate.AllYouCanEatRate == currentChildRate.AllYouCanEatRate && eatRate.NewAllYouCanEatRate == eatRate.ConfirmAllYouCanEatRate)
            {
                return true;
            }
            return false;
        }
        public bool ChangeTaxRateSQL(TaxRate taxRate)
        {
            var currentRate = _conn.QuerySingle<TaxRate>("SELECT * FROM TAX_RATE WHERE TAXRATEID = 1");
            if (taxRate.CurrentTaxRate == currentRate.CurrentTaxRate && taxRate.NewTaxRate == taxRate.ConfirmNewTaxRate)
            {
                return true;
            }
            return false;
        }
    }
}
