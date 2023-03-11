using Dapper;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class FoodBevIngredientRepository : IFoodBevIngredientRepository
    {
        private readonly IDbConnection _conn;

        public FoodBevIngredientRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<FoodBevIngredient> GetAllFoodBevIngredientsSQL()
        {
            return _conn.Query<FoodBevIngredient>("SELECT * FROM FOOD_BEV_INGREDIENTS;");
        }

        public FoodBevIngredient GetFoodBevIngredientSQL(int ingredientID)
        {
            return _conn.QuerySingle<FoodBevIngredient>("SELECT * FROM FOOD_BEV_INGREDIENTS WHERE INGREDIENTID = @id;", new { id = ingredientID });
        }

        public void UpdateFoodBevIngredientSQL(FoodBevIngredient ingredient)
        {
            _conn.Execute("UPDATE FOOD_BEV_INGREDIENTS SET INGREDIENTNAME = @name, INGREDIENTSTOCKLEVEL = @stocklevel, INGREDIENTCOST = @cost, INGREDIENTCATEGORYNAME = @categoryname WHERE INGREDIENTID = @id",
                new { name = ingredient.IngredientName, stocklevel = ingredient.IngredientStockLevel, cost = ingredient.IngredientCost, categoryname = ingredient.IngredientCategoryName, id = ingredient.IngredientID });
        }

        public void InsertFoodBevIngredientSQL(FoodBevIngredient ingredientToInsert)
        {
            _conn.Execute("INSERT INTO FOOD_BEV_INGREDIENTS (INGREDIENTNAME, INGREDIENTSTOCKLEVEL, INGREDIENTCOST, INGREDIENTCATEGORYNAME) VALUES (@name, @stocklevel, @cost, @categoryname);",
                new { name = ingredientToInsert.IngredientName, stocklevel = ingredientToInsert.IngredientStockLevel, cost = ingredientToInsert.IngredientCost, categoryname = ingredientToInsert.IngredientCategoryName });
        }

        public IEnumerable<FoodBevIngredientCategory> GetFoodBevCategoriesSQL()
        {
            return _conn.Query<FoodBevIngredientCategory>("SELECT * FROM INGREDIENTS_CATEGORIES");
        }

        public FoodBevIngredient AssignFoodBevIngredientCategorySQL()
        {
            var ingredientsCategoryList = GetFoodBevCategoriesSQL();
            var ingredient = new FoodBevIngredient();
            ingredient.IngredientCategories = ingredientsCategoryList;
            return ingredient;
        }

        public void DeleteFoodBevIngredientSQL(FoodBevIngredient ingredient)
        {
            _conn.Execute("DELETE FROM FOOD_BEV_INGREDIENTS WHERE INGREDIENTID = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID1 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID2 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID3 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID4 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID5 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID6 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID7 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID8 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID9 = @id;", new { id = ingredient.IngredientID });
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE INGREDIENTID10 = @id;", new { id = ingredient.IngredientID });
        }
    }
}
