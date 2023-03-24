using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject.Controllers
{
    public class FoodBevIngredientController : Controller
    {
        private readonly IFoodBevIngredientRepository repo;

        public FoodBevIngredientController(IFoodBevIngredientRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var ingredients = repo.GetAllFoodBevIngredientsSQL();
            return View(ingredients);
        }

        public IActionResult ViewFoodBevIngredient(int id)
        {
            var ingredient = repo.GetFoodBevIngredientSQL(id);
            return View(ingredient);
        }

        public IActionResult UpdateFoodBevIngredient(FoodBevIngredient id)
        {
            var categories = repo.AssignFoodBevIngredientCategorySQL();
            FoodBevIngredient ingredient = repo.GetFoodBevIngredientSQL(id.IngredientID);
            if (ingredient == null)
            {
                return View("ProductNotFound");
            }
            ingredient.IngredientCategories = categories.IngredientCategories;
            return View("UpdateFoodBevIngredient", ingredient);
        }

        public IActionResult UpdateFoodBevIngredientToDatabase(FoodBevIngredient ingredient)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateFoodBevIngredientSQL(ingredient);
                return RedirectToAction("ViewFoodBevIngredient", new { id = ingredient.IngredientID });
            }
            else
            {
                var categories = repo.AssignFoodBevIngredientCategorySQL();
                ingredient.IngredientCategories = categories.IngredientCategories;
                return View("UpdateFoodBevIngredient", ingredient);
            }
                
        }
        public IActionResult ResetAllIngredientsToBaseLevel()
        {
            return View("ResetInventoryLevelsToBase");
        }
        public IActionResult ResetAllIngredientsToBaseLevelToDatabase()
        {
            repo.ResetAllIngredientsToBaseLevelSQL();
            return RedirectToAction("Index");
        }
        public IActionResult InsertFoodBevIngredient()
        {
            var ingredient = repo.AssignFoodBevIngredientCategorySQL();
            return View(ingredient);
        }

        [HttpPost]
        public IActionResult InsertFoodBevIngredientToDatabase(FoodBevIngredient ingredientToInsert)
        {
            var ingr = repo.AssignFoodBevIngredientCategorySQL();
            ingredientToInsert.IngredientCategories = ingr.IngredientCategories;
            if (ModelState.IsValid)
            {
                repo.InsertFoodBevIngredientSQL(ingredientToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return View("InsertFoodBevIngredient", ingredientToInsert);
            }
        }

        public IActionResult DeleteFoodBevIngredient(FoodBevIngredient ingredient)
        {
            repo.DeleteFoodBevIngredientSQL(ingredient);
            return RedirectToAction("Index");
        }
    }
}
