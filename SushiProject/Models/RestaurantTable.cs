namespace SushiProject.Models
{
    public class RestaurantTable
    {
        public int RestaurantTableID { get; set; }
        public int RestaurantTableAssignedEmployeeID { get; set; }
        public int RestaurantTableAssignedEmployeeFirstName { get; set; }
        public int RestaurantTableAssignedEmployeeLastName { get; set; }
        public IEnumerable<Employee>? ServerList { get; set; }
    }
}
