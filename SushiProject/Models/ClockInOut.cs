using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class ClockInOut
    {
        public int LogInOutID { get; set; }
        public string? InOrOut { get; set; }

        [Required(ErrorMessage = "Employee ID entered is invalid.")]
        [Range(0, int.MaxValue, ErrorMessage = "Employee ID entered is invalid.")]
        public int? EmployeeID { get; set;}

        [Required(ErrorMessage = "Password entered is invalid.")]
        public string? Password { get; set; }
        public DateTime DateAndTime { get; set; }
        public IEnumerable<ClockInOut>? ClockInOutHistory { get; set; }
        public bool Success { get; set; }
    }
}
