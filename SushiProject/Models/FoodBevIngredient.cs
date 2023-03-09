using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class FoodBevIngredient
    {
        public int IngredientID { get; set; }

        [Required (ErrorMessage = "Please enter a valid ingredient name")]
        public string IngredientName { get; set; }
        [Required(ErrorMessage = "Please enter a valid initial stock level")]
        [Range (0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int IngredientStockLevel { get; set; }
        [Required(ErrorMessage = "Please enter a valid ingredient cost per unit")]
        [DataType(DataType.Currency)]
        public double IngredientCost { get; set; }
        [Required(ErrorMessage = "Please enter a valid category name")]
        public string IngredientCategoryName { get; set; }
        public IEnumerable<FoodBevIngredientCategory> IngredientCategories { get; set; }
    }
}
