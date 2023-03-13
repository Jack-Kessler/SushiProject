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

        public IActionResult ViewRestaurantTable(int RestaurantTableID) //IMPORTANT: The variable name here needs to be "RestaurantTableID". If it was simply "id" or soemthing different, it does not work.
        {
            var table = repo.GetRestaurantTableSQL(RestaurantTableID);
            return View(table);
        }

        public IActionResult UpdateRestaurantTable(int RestaurantTableID)
        {
            RestaurantTable updateTable = repo.GetRestaurantTableSQL(RestaurantTableID);
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
            var listOfServers = repo.GetServerListSQL(); //returns a list of objects type Employee

            foreach(var server in listOfServers)
            {
                if (tableToUpdate.RestaurantTableAssignedEmployeeID == server.EmployeeID)
                {
                    tableToUpdate.RestaurantTableAssignedEmployeeLastName = server.LastName;
                    tableToUpdate.RestaurantTableAssignedEmployeeFirstName = server.FirstName;
                    break;
                }
            }
            repo.UpdateRestaurantTableSQL(tableToUpdate); //Don't need to verify modelstate because the only option to change is a dropdown which guarantees data validation
            return RedirectToAction("ViewRestaurantTable", new { restaurantTableID = tableToUpdate.RestaurantTableID });
        }

        //public IActionResult InsertRestaurantTable()
        //{
        //    var table = repo.ServerListForTableSQL();
        //    return View(table);
        //}

        [HttpPost]
        public IActionResult InsertRestaurantTableToDatabase()
        {
            var table = repo.ServerListForTableSQL();
            repo.InsertRestaurantTableSQL(table);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteRestaurantTable(RestaurantTable table)
        {
            repo.DeleteRestaurantTableSQL(table);
            return RedirectToAction("Index");
        }
    }
}
