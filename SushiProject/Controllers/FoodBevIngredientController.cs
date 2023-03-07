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

        public IActionResult UpdateFoodBevIngredient(int id)
        {
            FoodBevIngredient ingredient = repo.GetFoodBevIngredientSQL(id);
            if (ingredient == null)
            {
                return View("ProductNotFound");
            }
            return View(ingredient);
        }

        public IActionResult UpdateFoodBevIngredientToDatabase(FoodBevIngredient ingredient)
        {
            repo.UpdateFoodBevIngredientSQL(ingredient);

            return RedirectToAction("ViewFoodBevIngredient", new { id = ingredient.IngredientID });
        }

        public IActionResult InsertFoodBevIngredient()
        {
            var ingredient = repo.AssignFoodBevIngredientCategorySQL();
            return View(ingredient);
        }

        public IActionResult InsertFoodBevIngredientToDatabase(FoodBevIngredient ingredientToInsert)
        {
            repo.InsertFoodBevIngredientSQL(ingredientToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(FoodBevIngredient ingredient)
        {
            repo.DeleteFoodBevIngredientSQL(ingredient);
            return RedirectToAction("Index");
        }
    }
}
