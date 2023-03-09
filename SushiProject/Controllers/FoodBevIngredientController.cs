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
            var categories = repo.AssignFoodBevIngredientCategorySQL();
            FoodBevIngredient ingredient = repo.GetFoodBevIngredientSQL(id);
            if (ingredient == null)
            {
                return View("ProductNotFound");
            }
            ingredient.IngredientCategories = categories.IngredientCategories;
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


        [HttpPost]
        public IActionResult InsertFoodBevIngredientToDatabase(FoodBevIngredient ingredientToInsert)
        {
            //var ingr = repo.AssignFoodBevIngredientCategorySQL();
            //ingredientToInsert.IngredientCategories = ingr.IngredientCategories;
            if (ModelState.IsValid)
            {
                repo.InsertFoodBevIngredientSQL(ingredientToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ingredientToInsert);
            }
        }

        public IActionResult DeleteFoodBevIngredient(FoodBevIngredient ingredient)
        {
            repo.DeleteFoodBevIngredientSQL(ingredient);
            return RedirectToAction("Index");
        }
    }
}
