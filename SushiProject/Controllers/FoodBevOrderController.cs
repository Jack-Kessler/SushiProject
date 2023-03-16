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

            repo.UpdateFoodBevOrderSQL(foodBevOrderToUpdate);
            return RedirectToAction("ViewFoodBevOrder", new { orderID = foodBevOrderToUpdate.OrderID });

            //if (ModelState.IsValid)
            //{
            //    repo.UpdateFoodBevOrderSQL(foodBevOrderToUpdate);
            //    return RedirectToAction("ViewFoodBevOrder", new { orderID = foodBevOrderToUpdate.OrderID });
            //}
            //else
            //{
            //    return View("UpdateFoodBevOrder", foodBevOrderToUpdate);
            //}
        }

        public IActionResult InsertFoodBevOrder(int transactionID)
        {
            FoodBevOrder order = new FoodBevOrder();

            order.TransactionID = transactionID;
            order.EmployeeID = repo.GetServerSQL(transactionID);
            order.TableID = repo.GetRestaurantTableSQL(transactionID);

            var menuItemList = repo.AssignMenuItemListSQL();
            order.MenuItemList = menuItemList.MenuItemList;

            return View(order);
        }

        [HttpPost]
        public IActionResult InsertFoodBevOrderToDatabase(FoodBevOrder foodBevOrderToInsert)
        {
            foodBevOrderToInsert.DateAndTime = DateTime.Now;

            foodBevOrderToInsert.OrderSaleAmount = repo.CalculateOrderPrice(foodBevOrderToInsert);
           
            repo.InsertFoodBevOrderSQL(foodBevOrderToInsert);

            int orderNum = repo.RetrieveOrderNumSQL(foodBevOrderToInsert.TransactionID, foodBevOrderToInsert.DateAndTime);
            foodBevOrderToInsert.OrderID = orderNum;

            int nullOrderSlot = repo.FindFirstNullOrderSlotSQL(foodBevOrderToInsert.TransactionID);

            repo.ApplyOrderToTransactionSQL(foodBevOrderToInsert, nullOrderSlot);

            return RedirectToAction("CustomerHomePage", "SalesTransaction", new { transactionID = foodBevOrderToInsert.TransactionID }); 
            //Pass in Transaction ID and redirect to SalesTransaction Controller -> CustomerHomePage
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
