using SushiProject.Models;

namespace SushiProject
{
    public interface IAccessRepository
    {
        public IEnumerable<Employee> GetAllEmployeeInfoSQL();
        public Employee GetEmployeeInfoSQL(int employeeID);
        public void UpdateEmployeeInfoSQL(Employee employeeToUpdate);
        public void InsertEmployeeInfoSQL(Employee employeeToInsert);
        public IEnumerable<EmployeeCategory> GetEmployeeRoleCategoriesSQL();
        public Employee AssignEmployeeRoleCategorySQL();
        public void DeleteEmployeeInfoSQL(Employee employeeToDelete);
        public IEnumerable<int> GetRangeOfEmployeeIDs();
        public bool ValidateEmployeeID(int employeeID);
        public bool UserPassAuthenticate(Employee employeeToAuthenticate);
    }
}
