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
            var tables = repo.GetAllRestaurantTablesSQL();
            return View(tables);
        }

        public IActionResult ViewRestaurantTable(int tableID)
        {
            var item = repo.GetRestaurantTableSQL(tableID);
            return View(item);
        }

        public IActionResult UpdateRestaurantTable(int tableID)
        {
            RestaurantTable updateTable = repo.GetRestaurantTableSQL(tableID);
            var tableWithServerList = repo.ServerListForTableSQL();
            updateTable.ServerList = tableWithServerList.ServerList;
           
            if (updateTable == null)
            {
                return View("ProductNotFound");
            }
            return View(updateTable);
        }

        public IActionResult UpdateRestaurantTableToDatabase(RestaurantTable tableToUpdate)
        {
            var tableWithServerList = repo.ServerListForTableSQL(); //needed since it can be null?
            tableToUpdate.ServerList = tableWithServerList.ServerList; //needed since it can be null?

            if (ModelState.IsValid)
            {
                repo.UpdateRestaurantTableSQL(tableToUpdate);
                return RedirectToAction("ViewRestaurantTable", new { id = tableToUpdate.RestaurantTableID });
            }
            else
            {
                return View("UpdateRestaurantTable", tableToUpdate);
            }
        }

        public IActionResult InsertRestaurantTable()
        {
            var table = repo.ServerListForTableSQL();
            return View(table);
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
