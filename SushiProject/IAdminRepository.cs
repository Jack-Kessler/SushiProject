using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject
{
    public interface IAdminRepository
    {
        public bool ChangeCustomerLogoutPasswordSQL(CustomerLogoutPassword password);
        public bool ChangeAllYouCanEatRateAdultSQL(AllYouCanEat eatRate);
        public bool ChangeAllYouCanEatRateChildSQL(AllYouCanEat eatRate);
        public bool ChangeTaxRateSQL(TaxRate taxRate);
    }
}
