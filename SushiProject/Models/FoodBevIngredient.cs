using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class FoodBevIngredient
    {
        public int IngredientID { get; set; }

        [Required (ErrorMessage = "Please enter a valid ingredient name")]
        [StringLength (50)] //Will not allow user to enter more than 50 chars.
        public string? IngredientName { get; set; }


        [Required(ErrorMessage = "Please enter a valid initial stock level between 0 and 2147483647")]
        [Range (0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int? IngredientStockLevel { get; set; }


        [Required(ErrorMessage = "Please enter a valid ingredient cost per unit between $0.00 - $99.00")]
        [DataType(DataType.Currency)]
        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public double? IngredientCost { get; set; }


        [Required(ErrorMessage = "Please enter a valid category name")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string? IngredientCategoryName { get; set; }

        public IEnumerable<FoodBevIngredientCategory>? IngredientCategories { get; set; }
    }
}
