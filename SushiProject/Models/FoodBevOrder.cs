using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SushiProject.Models
{
    public class FoodBevOrder
    {
        public int OrderID { get; set; }
        public int TransactionID { get; set; }
        public bool OrderFulfilled { get; set; }
        public int EmployeeID { get; set; }
        public int TableID { get; set; }
        public DateTime DateAndTime { get; set; }

        public IEnumerable<MenuItem>? MenuItemList { get; set; } //Note this is null

        public IEnumerable<Employee>? ServerList { get; set; } //Note this is null

        public IEnumerable<RestaurantTable>? RestaurantTableList { get; set; } //Note this is null

        //[Required(ErrorMessage = "Please enter a valid ingredient cost per unit between $0.00 - $99.00")]
        //[Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal OrderSaleAmount { get; set; }


        [Required(ErrorMessage = "Please enter a valid menu item name")]
        public string MenuItemName1 { get; set; }

        [Required(ErrorMessage = "Please enter a quantity between 0 and 10")]
        [Range(0, 10, ErrorMessage = "Please enter a quantity between 0 and 10")]
        public int QuantityItem1 { get; set; }

        [Required(ErrorMessage = "Please enter a valid menu item price between $0.00 - $99.00")]
        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal PriceItem1 { get; set; }



        public string MenuItemName2 { get; set; }

        [Range(0, 10, ErrorMessage = "Please enter a quantity between 0 and 10")]
        public int QuantityItem2 { get; set; }

        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal PriceItem2 { get; set; }



        public string MenuItemName3 { get; set; }

        [Range(0, 10, ErrorMessage = "Please enter a quantity between 0 and 10")]
        public int QuantityItem3 { get; set; }

        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal PriceItem3 { get; set; }



        public string MenuItemName4 { get; set; }

        [Range(0, 10, ErrorMessage = "Please enter a quantity between 0 and 10")]
        public int QuantityItem4 { get; set; }

        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal PriceItem4 { get; set; }
    }
}
