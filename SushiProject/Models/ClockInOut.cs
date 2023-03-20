namespace SushiProject.Models
{
    public class ClockInOut
    {
        public int LogInOutID { get; set; }
        public string InOrOut { get; set; }
        public int EmployeeID { get; set;}
        public DateTime DateAndTime { get; set; }
        public IEnumerable<ClockInOut> ClockInOutHistory { get; set; }
    }
}
