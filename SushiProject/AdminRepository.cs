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
            if (password.CurrentPassword == currentPass.CurrentPassword && password.NewPassword == password.ConfirmPassword)
            {
                return true;
            }
            return false;
        }
        public void UpdateCustomerLogoutPasswordInDatabaseSQl(CustomerLogoutPassword password)
        {
            _conn.Execute("UPDATE CUSTOMER_LOGOUT_PASSWORD SET CURRENTPASSWORD = @pass WHERE CUSTOMERLOGOUTPASSWORDID = 1;", new { pass = password.NewPassword });
        }
        public bool ChangeAllYouCanEatRateSQL(AllYouCanEat eatRate)
        {
            var currentRate = new AllYouCanEat();
            if (eatRate.Adult == true)
            {
                currentRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 1");
            }
            else
            {
                currentRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 2");
            }

            if (eatRate.AllYouCanEatRate == currentRate.AllYouCanEatRate && eatRate.NewAllYouCanEatRate == eatRate.ConfirmAllYouCanEatRate)
            {
                return true;
            }
            return false;

        }
        public void UpdateAllYouCanEatRateInDatabaseSQl(AllYouCanEat eatRate)
        {
            if(eatRate.Adult == true)
            {
                _conn.Execute("UPDATE ALL_YOU_CAN_EAT SET ALLYOUCANEATRATE = @eat WHERE ALLYOUCANEATID = 1;", new { eat = eatRate.NewAllYouCanEatRate });
            }
            else
            {
                _conn.Execute("UPDATE ALL_YOU_CAN_EAT SET ALLYOUCANEATRATE = @eat WHERE ALLYOUCANEATID = 2;", new { eat = eatRate.NewAllYouCanEatRate });
            }
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
        public void UpdateTaxRateInDatabaseSQl(TaxRate taxRate)
        {
            _conn.Execute("UPDATE TAX_RATE SET CURRENTTAXRATE = @tax WHERE TAXRATEID = 1;", new { tax = taxRate.NewTaxRate });
        }
        public void InsertClockInToDatabase(int employeeID)
        {
            var dateTimeNow = DateTime.Now;
            _conn.Execute("INSERT INTO LOG_IN_OUT (INOROUT, EMPLOYEEID, DATEANDTIME) VALUES(@inout, @eID, @date);", new { inout = "IN", eID = employeeID, date = dateTimeNow });
            _conn.Execute("UPDATE EMPLOYEES SET CLOCKEDINOROUT = 'IN', MOSTRECENTCLOCKINOUT = @time WHERE EMPLOYEEID = id;", new { time = dateTimeNow, id = employeeID });
        }
        public void InsertClockOutToDatabase(int employeeID)
        {
            var dateTimeNow = DateTime.Now;
            _conn.Execute("INSERT INTO LOG_IN_OUT (INOROUT, EMPLOYEEID, DATEANDTIME) VALUES(@inout, @eID, @date);", new { inout = "OUT", eID = employeeID, date = dateTimeNow });
            _conn.Execute("UPDATE EMPLOYEES SET CLOCKEDINOROUT = 'OUT', MOSTRECENTCLOCKINOUT = @time WHERE EMPLOYEEID = id;", new { time = dateTimeNow, id = employeeID });
        }
        public IEnumerable<ClockInOut> GetEmployeeClockInOutHistory(int employeeID)
        {
            var history = _conn.Query<ClockInOut>("SELECT * FROM LOG_IN_OUT WHERE EMPLOYEEID = @id;", new {id = employeeID});
            return history;
        }
        public bool ValidateEmployeeAndPasswordSQL(ClockInOut employee)
        {
            Employee credentials = new Employee();
            try
            {
                credentials = _conn.QuerySingle<Employee>("SELECT * FROM EMPLOYEES WHERE EMPLOYEEID = @id;", new { id = employee.EmployeeID });
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            if (credentials.EmployeeID == employee.EmployeeID && credentials.Password == employee.Password)
            {
                return true;
            }
            return false;
        }
        public IEnumerable<Employee> GetAllClockedInOutStaff()
        {
            var employeeList = _conn.Query<Employee>("SELECT * FROM EMPLOYEES;");
            return employeeList;
        }
    }
}
