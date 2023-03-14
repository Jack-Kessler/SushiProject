using Dapper;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class FoodBevOrderRepository : IFoodBevOrderRepository
    {
        private readonly IDbConnection _conn;

        public FoodBevOrderRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<FoodBevOrder> GetAllFoodBevOrdersSQL()
        {
            return _conn.Query<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS;");
        }
        public FoodBevOrder GetFoodBevOrderSQL(int foodBevOrderID)
        {
            return _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @id;", new { id = foodBevOrderID });
        }
        public void UpdateFoodBevOrderSQL(FoodBevOrder foodBevOrderToUpdate)
        {
            _conn.Execute("UPDATE FOOD_BEV_ORDERS SET EMPLOYEEID = @eID, TABLEID = @tID, ORDERSALEAMOUNT = @saleamt, MENUITEMNAME1 = @m1, QUANTITYITEM1 = @q1, PRICEITEM1 = @p1, MENUITEMNAME2 = @m2, QUANTITYITEM2 = @q2, PRICEITEM2 = @p2, MENUITEMNAME3 = @m3, QUANTITYITEM3 = @q3, PRICEITEM3 = @p3, MENUITEMNAME4 = @m4, QUANTITYITEM4 = @q4, PRICEITEM4 = @p4 WHERE ORDERID = @oID;",
               new { eID = foodBevOrderToUpdate.EmployeeID, tID = foodBevOrderToUpdate.TableID, saleamt = foodBevOrderToUpdate.OrderSaleAmount, m1 = foodBevOrderToUpdate.MenuItemName1, q1 = foodBevOrderToUpdate.QuantityItem1, p1 = foodBevOrderToUpdate.PriceItem1, m2 = foodBevOrderToUpdate.MenuItemName2, q2 = foodBevOrderToUpdate.QuantityItem2, p2 = foodBevOrderToUpdate.PriceItem2, m3 = foodBevOrderToUpdate.MenuItemName3, q3 = foodBevOrderToUpdate.QuantityItem3, p3 = foodBevOrderToUpdate.PriceItem3, m4 = foodBevOrderToUpdate.MenuItemName4, q4 = foodBevOrderToUpdate.QuantityItem4, p4 = foodBevOrderToUpdate.PriceItem4, oID = foodBevOrderToUpdate.OrderID});
        }
        public void InsertFoodBevOrderSQL(FoodBevOrder foodBevOrderToInsert)
        {
            _conn.Execute("INSERT INTO FOOD_BEV_ORDERS (EmployeeID, TableID, DateAndTime, OrderSaleAmount, MenuItemName1, QuantityItem1, PriceItem1, MenuItemName2, QuantityItem2, PriceItem2, MenuItemName3, QuantityItem3, PriceItem3, MenuItemName4, QuantityItem4, PriceItem4) VALUES(@eID, @tID, @date, @saleamt, @m1, @q1, @p1, @m2, @q2, @p2, @m3, @q3, @p3, @m4, @q4, @p4);", new { eID = foodBevOrderToInsert.EmployeeID, tID = foodBevOrderToInsert.TableID, date = foodBevOrderToInsert.DateAndTime, saleamt = foodBevOrderToInsert.OrderSaleAmount, m1 = foodBevOrderToInsert.MenuItemName1, q1 = foodBevOrderToInsert.QuantityItem1, p1 = foodBevOrderToInsert.PriceItem1, m2 = foodBevOrderToInsert.MenuItemName2, q2 = foodBevOrderToInsert.QuantityItem2, p2 = foodBevOrderToInsert.PriceItem2, m3 = foodBevOrderToInsert.MenuItemName3, q3 = foodBevOrderToInsert.QuantityItem3, p3 = foodBevOrderToInsert.PriceItem3, m4 = foodBevOrderToInsert.MenuItemName4, q4 = foodBevOrderToInsert.QuantityItem4, p4 = foodBevOrderToInsert.PriceItem4});
        }   
        public IEnumerable<MenuItem> GetMenuItemListSQL()
        {
            return _conn.Query<MenuItem>("SELECT * FROM MENU_CATEGORIES;");
        }
        public FoodBevOrder AssignMenuItemListSQL()
        {
            var menuItemList = GetMenuItemListSQL();
            var foodBevOrder = new FoodBevOrder();
            foodBevOrder.MenuItemList = menuItemList;
            return foodBevOrder;
        }
        public IEnumerable<Employee> GetServerListSQL()
        {
            return _conn.Query<Employee>("SELECT * FROM EMPLOYEES;");
        }
        public FoodBevOrder AssignServerListSQL()
        {
            var serverList = GetServerListSQL();
            var foodBevOrder = new FoodBevOrder();
            foodBevOrder.ServerList = serverList;
            return foodBevOrder;
        }
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL()
        {
            return _conn.Query<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES;");
        }
        public FoodBevOrder AssignRestaurantTableListSQL()
        {
            var tableList = GetRestaurantTableListSQL();
            var foodBevOrder = new FoodBevOrder();
            foodBevOrder.RestaurantTableList = tableList;
            return foodBevOrder;
        }
        public void DeleteFoodBevOrderSQL(FoodBevOrder foodBevOrderToDelete)
        {
            _conn.Execute("DELETE FROM FOOD_BEV_ORDERS WHERE ORDERID = @id;", new { id = foodBevOrderToDelete.OrderID });
        }
        public void FulfillFoodBevOrder(int foodBevOrderID)
        {
            _conn.Execute("UPDATE FOOD_BEV_ORDERS SET ORDERFULFILLED = @fulfilled WHERE ORDERID = @oID;",
               new { fulfilled = 1, oID = foodBevOrderID });
        }
    }
}
