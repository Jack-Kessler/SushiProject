using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SushiProject.Models;
using static Mysqlx.Notice.Warning.Types;

namespace SushiProject.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IMenuItemRepository repo;

        public MenuItemController(IMenuItemRepository repo)
        {
            this.repo = repo;
        }


        public IActionResult Index()
        {
            var items = repo.GetAllMenuItemsSQL();
            return View(items);
        }

        public IActionResult ViewMenuItem(int id)
        {
            var item = repo.GetMenuItemSQL(id);
            return View(item);
        }

        public IActionResult UpdateMenuItem(int id)
        {
            MenuItem item = repo.GetMenuItemSQL(id);
            if (item == null)
            {
                return View("ProductNotFound");
            }
            return View(item);
        }

        public IActionResult UpdateMenuItemToDatabase(MenuItem item)
        {
            repo.UpdateMenuItemSQL(item);
            return RedirectToAction("ViewMenuItem", new { id = item.MenuItemID });
        }

        public IActionResult InsertMenuItem()
        {
            var item = repo.AssignMenuItemCategorySQL();
            var ingredients = repo.AssignMenuItemIngredientListSQL();
            item.MenuItemIngredientList = ingredients;
            return View(item);
        }

        [HttpPost]
        public IActionResult InsertMenuItemToDatabase(MenuItem menuItemToInsert)
        {
            var menuItemCat = repo.AssignMenuItemCategorySQL();
            menuItemToInsert.MenuItemCategories = menuItemCat.MenuItemCategories;
            var ingredients = repo.AssignMenuItemIngredientListSQL();
            menuItemToInsert.MenuItemIngredientList = ingredients;
            menuItemToInsert = repo.IngredientSetNullValues(menuItemToInsert);


            if (ModelState.IsValid)
            {
                repo.InsertMenuItemSQL(menuItemToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return View("InsertMenuItem", menuItemToInsert);
            }
        }

        public IActionResult DeleteMenuItem(MenuItem menuItem)
        {
            repo.DeleteMenuItemSQL(menuItem);
            return RedirectToAction("Index");
        }
    }
}
