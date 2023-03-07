namespace SushiProject.Models
{
    public class FoodBevIngredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public int IngredientStockLevel { get; set; }
        public double IngredientCost { get; set; }
        public string IngredientCategoryName { get; set; }
        public IEnumerable<FoodBevIngredientCategory> IngredientCategories { get; set; }
    }
}
