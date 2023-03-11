using Dapper;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class AccessRepository : IAccessRepository
    {
        private readonly IDbConnection _conn;

        public AccessRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Employee> GetAllEmployeeInfoSQL()
        {
            return _conn.Query<Employee>("SELECT * FROM EMPLOYEES;");
        }

        public Employee GetEmployeeInfoSQL(int employeeID)
        {
            return _conn.QuerySingle<Employee>("SELECT * FROM EMPLOYEES WHERE EMPLOYEEID = @id;", new { id = employeeID });
        }

        public void UpdateEmployeeInfoSQL(Employee employeeToUpdate)
        {
            _conn.Execute("UPDATE EMPLOYEES SET FirstName = @fName, " +
                "          MiddleName = @mName, " +
                "          LastName = @lName, " +
                "          EmailAddress = @email, " +
                "          PhoneNumber = @phone, " +
                "          DateOfBirth = @dob, " +
                "          UserName = @user, " +
                "          Password = @pass, " +
                "          Role = @role" +
                "          WHERE EMPLOYEEID = @id;",
                new { fname = employeeToUpdate.FirstName, 
                      mName = employeeToUpdate.MiddleName, 
                      lName = employeeToUpdate.LastName, 
                      email = employeeToUpdate.EmailAddress, 
                      phone = employeeToUpdate.PhoneNumber, 
                      dob = employeeToUpdate.DateOfBirth,
                      user = employeeToUpdate.UserName,
                      pass = employeeToUpdate.Password,
                      role = employeeToUpdate.Role});
        }

        public void InsertEmployeeInfoSQL(Employee employeeToInsert)
        {
            _conn.Execute("INSERT INTO EMPLOYEES " +
                "         (FirstName, MiddleName, LastName, EmailAddress, PhoneNumber, DateOfBirth, UserName, Password, Role) VALUES (fName, mName, lName, email, phone, dob, user, pass, role);", new
               {
                   fname = employeeToInsert.FirstName,
                   mName = employeeToInsert.MiddleName,
                   lName = employeeToInsert.LastName,
                   email = employeeToInsert.EmailAddress,
                   phone = employeeToInsert.PhoneNumber,
                   dob = employeeToInsert.DateOfBirth,
                   user = employeeToInsert.UserName,
                   pass = employeeToInsert.Password,
                   role = employeeToInsert.Role
               });
        }

        public IEnumerable<EmployeeCategory> GetEmployeeRoleCategoriesSQL()
        {
            return _conn.Query<EmployeeCategory>("SELECT * FROM EMPLOYEE_CATEGORIES;");
        }
        public Employee AssignEmployeeRoleCategorySQL()
        {
            var employeeCategoryList = GetEmployeeRoleCategoriesSQL();
            var employee = new Employee();
            employee.EmployeeCategories = employeeCategoryList;
            return employee;
        }

        public void DeleteEmployeeInfoSQL(Employee employeeToDelete)
        {
            _conn.Execute("DELETE FROM EMPLOYEES WHERE EMPLOYEEID = @id;", new { id = employeeToDelete.EmployeeID });
        }

        public IEnumerable<int> GetRangeOfEmployeeIDs() //Note: employeeToAuthenticate only contains a userName and password
        {
            IEnumerable<int> range = (IEnumerable<int>)_conn.Query<Employee>("SELECT (EMPLOYEEID) FROM EMPLOYEES;");

            return range;
        }

        public bool ValidateEmployeeID(int employeeID)
        {
            var range = GetRangeOfEmployeeIDs();
            foreach (var item in range)
            {
                if (employeeID == item) //Checks if 
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserPassAuthenticate(Employee employeeToAuthenticate) //Note: employeeToAuthenticate only contains a userName and password
        {
            if (employeeToAuthenticate.EmployeeID == null)
            {
                return false;
            }

            bool success = ValidateEmployeeID(employeeToAuthenticate.EmployeeID); //Makes sure employee ID exists.
            if(success == false)
            {
                return false;
            }

            var employeeInfo = GetEmployeeInfoSQL(employeeToAuthenticate.EmployeeID);
            if(employeeInfo.UserName == employeeToAuthenticate.UserName && employeeInfo.Password == employeeToAuthenticate.Password)
            {
                return true;
            }
            return false;
        }
    }
}
