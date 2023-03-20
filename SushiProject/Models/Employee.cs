using Microsoft.VisualBasic;
using System.Formats.Asn1;

namespace SushiProject.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool KeepLoggedIn { get; set; }
        public bool Guest { get; set; }
        public IEnumerable<EmployeeCategory>? EmployeeCategories { get; set; } //Note this is null
        public string ClockedInOrOut { get; set; }
        public DateTime MostRecentClockInOut { get; set; }
    }
}
