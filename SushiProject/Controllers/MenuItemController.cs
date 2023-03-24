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
        public async Task<IActionResult> ViewAllMenuItems(int transactionID, string SearchString, string SearchStringIngredient) //For Customer viewing all menu items
        {
            var allMenuItems = repo.GetAllMenuItemsSQL();
            var menuItem = new MenuItem();

            menuItem.TransactionID = transactionID;

            ViewData["CurrentFilter"] = SearchString;
            ViewData["CurrentFilterIngredient"] = SearchStringIngredient;
            var menuItems = from m in allMenuItems select m;
            if (!String.IsNullOrEmpty(SearchString))
            {
                //menuItems = menuItems.Where(m => m.MenuItemName.Contains(SearchString)); //Code here was case sensitive.
                menuItems = menuItems.Where(m => m.MenuItemName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0); //Code here makes search NOT case sensitive.
            }
            else if (!String.IsNullOrEmpty(SearchStringIngredient))
            {
                menuItems = menuItems.Where(m => m.MenuItemIngredientName1.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                            (m.MenuItemIngredientName2 != null && m.MenuItemIngredientName2.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName3 != null && m.MenuItemIngredientName3.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName4 != null && m.MenuItemIngredientName4.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName5 != null && m.MenuItemIngredientName5.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName6 != null && m.MenuItemIngredientName6.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName7 != null && m.MenuItemIngredientName7.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName8 != null && m.MenuItemIngredientName8.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName9 != null && m.MenuItemIngredientName9.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                            (m.MenuItemIngredientName10 != null && m.MenuItemIngredientName10.IndexOf(SearchStringIngredient, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            menuItem.AllMenuItems = menuItems;
            return View("CustomerMenuItems", menuItem);
        }
        public IActionResult ViewMenuItem(int id)
        {
            var item = repo.GetMenuItemSQL(id);
            return View(item);
        }
        public IActionResult UpdateMenuItem(MenuItem item)
        {
            MenuItem updateItem = repo.GetMenuItemSQL(item.MenuItemID);//These two lines of code ensure MenuItemCategory is not null
            var menuItemCat = repo.AssignMenuItemCategorySQL();
            updateItem.MenuItemCategories = menuItemCat.MenuItemCategories;//These two lines of code ensure MenuItemIngredientList is not null
            var ingredients = repo.AssignMenuItemIngredientListSQL();
            updateItem.MenuItemIngredientList = ingredients;
            //updateItem = repo.IngredientSetNullValues(updateItem);
            if (updateItem == null)
            {
                return View("ProductNotFound");
            }
            return View(updateItem);
        }
        public IActionResult UpdateMenuItemToDatabase(MenuItem menuItemToUpdate)
        {
            var menuItemCat = repo.AssignMenuItemCategorySQL();
            menuItemToUpdate.MenuItemCategories = menuItemCat.MenuItemCategories;
            var ingredients = repo.AssignMenuItemIngredientListSQL();
            menuItemToUpdate.MenuItemIngredientList = ingredients;
            menuItemToUpdate = repo.IngredientSetNullValues(menuItemToUpdate);

            if (ModelState.IsValid)
            {
                repo.UpdateMenuItemSQL(menuItemToUpdate);
                return RedirectToAction("ViewMenuItem", new { id = menuItemToUpdate.MenuItemID });
            }
            else
            {
                return View("UpdateMenuItem", menuItemToUpdate);
            }
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

            menuItemToInsert = repo.ValidateIngredients(menuItemToInsert);

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
