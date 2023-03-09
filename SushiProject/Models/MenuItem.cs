using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        [Required(ErrorMessage = "Please enter a valid menu item name")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string MenuItemName { get; set; }

        [Required(ErrorMessage = "Please enter a valid menu item price between $0.00 - $99.00")]
        [DataType(DataType.Currency)]
        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public double MenuItemPrice { get; set; }

        public IEnumerable<MenuItemCategory>? MenuItemCategories { get; set; } //Note this is null

        public IEnumerable<FoodBevIngredient>? MenuItemIngredientCategories { get; set; } //Note this is null

        [Required(ErrorMessage = "Please enter a valid ingredient")]
        public string MenuItemIngredientName1 { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public string MenuItemIngredientID1 { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int MenuItemQuantityItem1 { get; set; }
        public string MMenuItemIngredientName2 { get; set; }

        public string MenuItemIngredientID2 { get; set; }

        public int MenuItemQuantityItem2 { get; set; }
        public string MMenuItemIngredientName3 { get; set; }

        public string MenuItemIngredientID3 { get; set; }

        public int MenuItemQuantityItem3 { get; set; }
        public string MMenuItemIngredientName4 { get; set; }

        public string MenuItemIngredientID4 { get; set; }

        public int MenuItemQuantityItem4 { get; set; }
        public string MMenuItemIngredientName5 { get; set; }

        public string MenuItemIngredientID5 { get; set; }

        public int MenuItemQuantityItem5 { get; set; }
        public string MMenuItemIngredientName6 { get; set; }

        public string MenuItemIngredientID6 { get; set; }

        public int MenuItemQuantityItem6 { get; set; }
        public string MMenuItemIngredientName7 { get; set; }

        public string MenuItemIngredientID7 { get; set; }

        public int MenuItemQuantityItem7 { get; set; }
        public string MMenuItemIngredientName8 { get; set; }

        public string MenuItemIngredientID8 { get; set; }

        public int MenuItemQuantityItem8 { get; set; }
        public string MMenuItemIngredientName9 { get; set; }

        public string MenuItemIngredientID9 { get; set; }

        public int MenuItemQuantityItem9 { get; set; }
        public string MMenuItemIngredientName10 { get; set; }

        public string MenuItemIngredientID10 { get; set; }

        public int MenuItemQuantityItem10 { get; set; }
    }
}