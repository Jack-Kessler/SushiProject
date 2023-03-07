using SushiProject.Models;

namespace SushiProject
{
    public interface IFoodBevIngredientRepository
    {
        public IEnumerable<FoodBevIngredient> GetAllFoodBevIngredientsSQL();
        public FoodBevIngredient GetFoodBevIngredientSQL(int IngredientID);
        public void UpdateFoodBevIngredientSQL(FoodBevIngredient ingredient);
        public void InsertFoodBevIngredientSQL(FoodBevIngredient ingredientToInsert);
        public IEnumerable<FoodBevIngredientCategory> GetFoodBevCategoriesSQL();
        public FoodBevIngredient AssignFoodBevIngredientCategorySQL();
        public void DeleteFoodBevIngredientSQL(FoodBevIngredient ingredient);
    }
}
