using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject.Controllers
{
    public class RestaurantTableController : Controller
    {
        private readonly IRestaurantTableRepository repo;

        public RestaurantTableController(IRestaurantTableRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var items = repo.GetAllRestaurantTablesSQL();
            return View(items);
        }

        public IActionResult ViewRestaurantTable(int id)
        {
            var item = repo.GetRestaurantTableSQL(id);
            return View(item);
        }

        public IActionResult UpdateRestaurantTable(int id)
        {
            RestaurantTable updateTable = repo.GetMenuItemSQL(id);
           
            if (updateTable == null)
            {
                return View("ProductNotFound");
            }
            return View(updateTable);
        }

        public IActionResult UpdateRestaurantTableToDatabase(MenuItem tableToUpdate)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateRestaurantTableSQL(tableToUpdate);
                //return RedirectToAction("ViewMenuItem", new { id = menuItemToUpdate.MenuItemID }); Update this row
            }
            else
            {
                return View("UpdateRestaurantTable", tableToUpdate);
            }
        }

        public IActionResult InsertRestaurantTable() //NEED TO REDO
        {
            var item = repo.AssignMenuItemCategorySQL();
            var ingredients = repo.AssignMenuItemIngredientListSQL();
            item.MenuItemIngredientList = ingredients;
            return View(item);
        }

        [HttpPost]
        public IActionResult InsertRestaurantTableToDatabase(RestaurantTable tableToInsert)
        {
            if (ModelState.IsValid)
            {
                repo.InsertRestaurantTableSQL(tableToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return View("InsertRestaurantTable", tableToInsert);
            }
        }

        public IActionResult DeleteRestaurantTable(RestaurantTable table)
        {
            repo.DeleteRestaurantTableSQL(table);
            return RedirectToAction("Index");
        }
    }
}
