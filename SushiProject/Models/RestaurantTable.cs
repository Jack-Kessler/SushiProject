namespace SushiProject.Models
{
    public class RestaurantTable
    {
        public int RestaurantTableID { get; set; }
        public int? RestaurantTableAssignedEmployeeID { get; set; }
        public string? RestaurantTableAssignedEmployeeFirstName { get; set; }
        public string? RestaurantTableAssignedEmployeeLastName { get; set; }
        public IEnumerable<Employee>? ServerList { get; set; }
    }
}
