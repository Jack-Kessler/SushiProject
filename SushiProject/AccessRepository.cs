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
                "          EmailAddress = @email" +
                "          PhoneNumber = @phone" +
                "          DateOfBirth = @dob" +
                "          Role = @role" +
                "          WHERE EMPLOYEEID = @id",
                new { fname = employeeToUpdate.FirstName, 
                      mName = employeeToUpdate.MiddleName, 
                      lName = employeeToUpdate.LastName, 
                      email = employeeToUpdate.EmailAddress, 
                      phone = employeeToUpdate.PhoneNumber, 
                      dob = employeeToUpdate.DateOfBirth, 
                      role = employeeToUpdate.Role});
        }

        public Employee AssignEmployeeRoleCategorySQL()
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployeeInfoSQL(Employee employeeToDelete)
        {
            throw new NotImplementedException();
        }





        public IEnumerable<EmployeeCategory> GetEmployeeRoleCategoriesSQL()
        {
            throw new NotImplementedException();
        }

        public void InsertEmployeeInfoSQL(Employee employeeToInsert)
        {
            throw new NotImplementedException();
        }


    }
}
