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

        public IActionResult ViewCustomerFoodBevOrder(int transactionID)
        {
            var ordersList = new FoodBevOrder();
            var customerOrders = repo.GetCustomerFoodBevOrdersSQL(transactionID);
            ordersList.CustomerOrderList = customerOrders;
            ordersList.TransactionID = transactionID;
            return View("CustomerOrders", ordersList);
        }
        public IActionResult ViewOpenFoodBevOrders()
        {
            var openOrders = repo.GetAllOpenFoodBevOrdersSQL();
            return View("ChefOpenOrders", openOrders);
        }
        public IActionResult UpdateFoodBevOrder(int id)
        {
            FoodBevOrder updateOrder = repo.GetFoodBevOrderSQL(id);

            var order = repo.CreateShellFoodBevOrderSQL();

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
            //var order = repo.CreateShellFoodBevOrder();

            //foodBevOrderToUpdate.MenuItemList = order.MenuItemList;
            //foodBevOrderToUpdate.ServerList = order.ServerList;
            //foodBevOrderToUpdate.RestaurantTableList = order.RestaurantTableList;

            foodBevOrderToUpdate.DateAndTime = DateTime.Now;

            foodBevOrderToUpdate.OrderSaleAmount = repo.CalculateOrderPriceSQL(foodBevOrderToUpdate);

            repo.UpdateFoodBevOrderSQL(foodBevOrderToUpdate);
            return RedirectToAction("ViewFoodBevOrder", new { orderID = foodBevOrderToUpdate.OrderID });
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

            foodBevOrderToInsert.OrderSaleAmount = repo.CalculateOrderPriceSQL(foodBevOrderToInsert);
           
            repo.InsertFoodBevOrderSQL(foodBevOrderToInsert);

            int orderNum = repo.RetrieveOrderNumSQL(foodBevOrderToInsert.TransactionID, foodBevOrderToInsert.DateAndTime);
            foodBevOrderToInsert.OrderID = orderNum;

            int nullOrderSlot = repo.FindFirstNullOrderSlotSQL(foodBevOrderToInsert.TransactionID);

            repo.ApplyOrderToTransactionSQL(foodBevOrderToInsert, nullOrderSlot);

            return RedirectToAction("CustomerHomePage", "SalesTransaction", new { transactionID = foodBevOrderToInsert.TransactionID }); 
            //Line of code above passes in Transaction ID as argument and redirects to SalesTransaction Controller -> CustomerHomePage
        }

        public IActionResult DeleteFoodBevOrder(FoodBevOrder foodBevOrder)
        {
            repo.DeleteFoodBevOrderSQL(foodBevOrder);
            return RedirectToAction("Index");
        }

        public IActionResult FulfillFoodBevOrder (FoodBevOrder foodBevOrder)
        {
            FoodBevOrder order = repo.GetFoodBevOrderSQL(foodBevOrder.OrderID);
            repo.SubtractIngredientInventorySQL(order);
            repo.FulfillFoodBevOrderSQL(order);
            return RedirectToAction("Index");
        }
    }
}
