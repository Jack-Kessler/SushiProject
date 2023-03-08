using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiProject.Models
{
    public class FoodBevIngredient
    {
        public int IngredientID { get; set; }
        [Display(Name = "Ingredient Name")]
        [StringLength (50, MinimumLength = 1)]
        [Required]
        public string IngredientName { get; set; }
        [Display(Name = "Ingredient Stock Level")]
        [Required]
        public int IngredientStockLevel { get; set; }
        [Range(0.01, 99.99)]
        [Display(Name = "Ingredient Cost")]
        [DataType(DataType.Currency)]
        [Required]
        public double IngredientCost { get; set; }
        [Display(Name = "Ingredient Category Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string IngredientCategoryName { get; set; }
        public IEnumerable<FoodBevIngredientCategory> IngredientCategories { get; set; }
    }
}
