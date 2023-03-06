using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

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
            return View(item);
        }

        public IActionResult InsertMenuItemToDatabase(MenuItem menuItemToInsert)
        {
            repo.InsertMenuItemSQL(menuItemToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMenuItem(MenuItem menuItem)
        {
            repo.DeleteMenuItemSQL(menuItem);
            return RedirectToAction("Index");
        }
    }
}
