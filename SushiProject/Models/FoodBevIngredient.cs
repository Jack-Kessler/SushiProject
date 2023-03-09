using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class FoodBevIngredient
    {
        public int IngredientID { get; set; }

        [Required]
        public string IngredientName { get; set; }
        [Required]
        public int IngredientStockLevel { get; set; }
        [Required]
        public double IngredientCost { get; set; }
        [Required]
        public string IngredientCategoryName { get; set; }
        public IEnumerable<FoodBevIngredientCategory> IngredientCategories { get; set; }
    }
}
