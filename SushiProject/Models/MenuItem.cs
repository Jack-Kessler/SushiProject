using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        
        [Required(ErrorMessage = "Please enter a valid menu item name")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string? MenuItemName { get; set; }



        [Required(ErrorMessage = "Please enter a valid menu item price between $0.00 - $99.00")]
        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal MenuItemPrice { get; set; }



        //[Required(ErrorMessage = "Please enter a valid menu item category")]
        public string? MenuItemCategory { get; set; }

        public IEnumerable<MenuItemCategory>? MenuItemCategories { get; set; } //Note this is null

        public IEnumerable<FoodBevIngredient>? MenuItemIngredientList { get; set; } //Note this is null



        //[Required(ErrorMessage = "Please enter a valid ingredient")]
        public string? MenuItemIngredientName1 { get; set; }

        [Required(ErrorMessage = "Please enter a valid quantity from 1 - 2147483647")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int? MenuItemIngredientQuantity1 { get; set; }



        public string? MenuItemIngredientName2 { get; set; }
        public int? MenuItemIngredientQuantity2 { get; set; }


        public string? MenuItemIngredientName3 { get; set; }
        public int? MenuItemIngredientQuantity3 { get; set; }


        public string? MenuItemIngredientName4 { get; set; }
        public int? MenuItemIngredientQuantity4 { get; set; }


        public string? MenuItemIngredientName5 { get; set; }
        public int? MenuItemIngredientQuantity5 { get; set; }


        public string? MenuItemIngredientName6 { get; set; }
        public int? MenuItemIngredientQuantity6 { get; set; }


        public string? MenuItemIngredientName7 { get; set; }
        public int? MenuItemIngredientQuantity7 { get; set; }


        public string? MenuItemIngredientName8 { get; set; }
        public int? MenuItemIngredientQuantity8 { get; set; }


        public string? MenuItemIngredientName9 { get; set; }
        public int? MenuItemIngredientQuantity9 { get; set; }


        public string? MenuItemIngredientName10 { get; set; }
        public int? MenuItemIngredientQuantity10 { get; set; }
    }
}