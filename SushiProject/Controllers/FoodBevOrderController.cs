using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;

namespace SushiProject.Controllers
{
    public class FoodBevOrderController : Controller
    {
        private readonly IFoodBevOrderRepository repo;

        public FoodBevOrderController(IFoodBevOrderRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var orders = repo.GetAllFoodBevOrdersSQL();
            return View(orders);
        }

        public IActionResult ViewFoodBevOrder(int orderID) //Matched with index view - can change? -- need to try.
        {
            var order = repo.GetFoodBevOrderSQL(orderID);
            return View(order);
        }

        public IActionResult UpdateFoodBevOrder(int id)
        {
            FoodBevOrder updateOrder = repo.GetFoodBevOrderSQL(id);

            var order = repo.CreateShellFoodBevOrder();

            updateOrder.MenuItemList = order.MenuItemList;
            updateOrder.ServerList = order.ServerList;
            updateOrder.RestaurantTableList = order.RestaurantTableList;

            if (updateOrder == null)
            {
                return View("ProductNotFound");
            }
            return View(updateOrder);
        }

        public IActionResult UpdateFoodBevOrderToDatabase(FoodBevOrder foodBevOrderToUpdate)
        {
            var order = repo.CreateShellFoodBevOrder();

            foodBevOrderToUpdate.MenuItemList = order.MenuItemList;
            foodBevOrderToUpdate.ServerList = order.ServerList;
            foodBevOrderToUpdate.RestaurantTableList = order.RestaurantTableList;

            foodBevOrderToUpdate.DateAndTime = DateTime.Now;

            foodBevOrderToUpdate.OrderSaleAmount = repo.CalculateOrderPrice(foodBevOrderToUpdate);

            if (ModelState.IsValid)
            {
                repo.UpdateFoodBevOrderSQL(foodBevOrderToUpdate);
                return RedirectToAction("ViewFoodBevOrder", new { orderID = foodBevOrderToUpdate.OrderID });
            }
            else
            {
                return View("UpdateFoodBevOrder", foodBevOrderToUpdate);
            }
        }

        public IActionResult InsertFoodBevOrder()
        {
           var order = repo.CreateShellFoodBevOrder();

            return View(order);
        }

        [HttpPost]
        public IActionResult InsertFoodBevOrderToDatabase(FoodBevOrder foodBevOrderToInsert)
        {
            var order = repo.CreateShellFoodBevOrder();

            foodBevOrderToInsert.MenuItemList = order.MenuItemList;
            foodBevOrderToInsert.ServerList = order.ServerList;
            foodBevOrderToInsert.RestaurantTableList = order.RestaurantTableList;

            foodBevOrderToInsert.DateAndTime = DateTime.Now;

            foodBevOrderToInsert.OrderSaleAmount = repo.CalculateOrderPrice(foodBevOrderToInsert);
           
            repo.InsertFoodBevOrderSQL(foodBevOrderToInsert);
            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteFoodBevOrder(FoodBevOrder foodBevOrder)
        {
            repo.DeleteFoodBevOrderSQL(foodBevOrder);
            return RedirectToAction("Index");
        }

        public IActionResult FulfillFoodBevOrder (FoodBevOrder foodBevOrder)
        {
            repo.FulfillFoodBevOrderSQL(foodBevOrder);
            return RedirectToAction("Index");
        }
    }
}
