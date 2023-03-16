using Dapper;
using Mysqlx.Crud;
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
        public FoodBevOrder GetFoodBevOrderSQL(int OrderID)
        {
            return _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @id;", new { id = OrderID });
        }
        public IEnumerable<FoodBevOrder> GetCustomerFoodBevOrdersSQL(int transactionID)
        {
            return _conn.Query<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE TRANSACTIONID = @id;", new { id = transactionID });
        }
        public void UpdateFoodBevOrderSQL(FoodBevOrder foodBevOrderToUpdate)
        {
            _conn.Execute("UPDATE FOOD_BEV_ORDERS SET EMPLOYEEID = @eID, TABLEID = @tID, DATEANDTIME = @date, ORDERSALEAMOUNT = @saleamt, MENUITEMNAME1 = @m1, QUANTITYITEM1 = @q1, PRICEITEM1 = @p1, MENUITEMNAME2 = @m2, QUANTITYITEM2 = @q2, PRICEITEM2 = @p2, MENUITEMNAME3 = @m3, QUANTITYITEM3 = @q3, PRICEITEM3 = @p3, MENUITEMNAME4 = @m4, QUANTITYITEM4 = @q4, PRICEITEM4 = @p4 WHERE ORDERID = @oID;",
               new { eID = foodBevOrderToUpdate.EmployeeID, tID = foodBevOrderToUpdate.TableID, date = foodBevOrderToUpdate.DateAndTime, saleamt = foodBevOrderToUpdate.OrderSaleAmount, m1 = foodBevOrderToUpdate.MenuItemName1, q1 = foodBevOrderToUpdate.QuantityItem1, p1 = foodBevOrderToUpdate.PriceItem1, m2 = foodBevOrderToUpdate.MenuItemName2, q2 = foodBevOrderToUpdate.QuantityItem2, p2 = foodBevOrderToUpdate.PriceItem2, m3 = foodBevOrderToUpdate.MenuItemName3, q3 = foodBevOrderToUpdate.QuantityItem3, p3 = foodBevOrderToUpdate.PriceItem3, m4 = foodBevOrderToUpdate.MenuItemName4, q4 = foodBevOrderToUpdate.QuantityItem4, p4 = foodBevOrderToUpdate.PriceItem4, oID = foodBevOrderToUpdate.OrderID});
        }
        public void InsertFoodBevOrderSQL(FoodBevOrder foodBevOrderToInsert)
        {
            _conn.Execute("INSERT INTO FOOD_BEV_ORDERS (TransactionID, EmployeeID, TableID, DateAndTime, OrderSaleAmount, MenuItemName1, QuantityItem1, PriceItem1, MenuItemName2, QuantityItem2, PriceItem2, MenuItemName3, QuantityItem3, PriceItem3, MenuItemName4, QuantityItem4, PriceItem4) VALUES(@tran, @eID, @tID, @date, @saleamt, @m1, @q1, @p1, @m2, @q2, @p2, @m3, @q3, @p3, @m4, @q4, @p4);", new { tran =foodBevOrderToInsert.TransactionID, eID = foodBevOrderToInsert.EmployeeID, tID = foodBevOrderToInsert.TableID, date = foodBevOrderToInsert.DateAndTime, saleamt = foodBevOrderToInsert.OrderSaleAmount, m1 = foodBevOrderToInsert.MenuItemName1, q1 = foodBevOrderToInsert.QuantityItem1, p1 = foodBevOrderToInsert.PriceItem1, m2 = foodBevOrderToInsert.MenuItemName2, q2 = foodBevOrderToInsert.QuantityItem2, p2 = foodBevOrderToInsert.PriceItem2, m3 = foodBevOrderToInsert.MenuItemName3, q3 = foodBevOrderToInsert.QuantityItem3, p3 = foodBevOrderToInsert.PriceItem3, m4 = foodBevOrderToInsert.MenuItemName4, q4 = foodBevOrderToInsert.QuantityItem4, p4 = foodBevOrderToInsert.PriceItem4});
        }
        public int RetrieveOrderNumSQL(int transactionID, DateTime dateAndTime)
        {
            var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE TRANSACTIONID = @tID AND DATEANDTIME = @date;", new { tID = transactionID, date = dateAndTime });
            return order.OrderID;
        }
        public int FindFirstNullOrderSlotSQL(int transactionID)
        {
            var transaction = _conn.QuerySingle<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = transactionID });
            
            if (transaction.OrderID1 == 0) return 1;
            else if (transaction.OrderID2 == 0) return 2;
            else if (transaction.OrderID3 == 0) return 3;
            else if (transaction.OrderID4 == 0) return 4;
            else if (transaction.OrderID5 == 0) return 5;
            else if (transaction.OrderID6 == 0) return 6;
            else if (transaction.OrderID7 == 0) return 7;
            else if (transaction.OrderID8 == 0) return 8;
            else if (transaction.OrderID9 == 0) return 9;
            else if (transaction.OrderID10 == 0) return 10;
            else if (transaction.OrderID11 == 0) return 11;
            else if (transaction.OrderID12 == 0) return 12;
            else if (transaction.OrderID13 == 0) return 13;
            else if (transaction.OrderID14 == 0) return 14;
            else if (transaction.OrderID15 == 0) return 15;
            else if (transaction.OrderID16 == 0) return 16;
            else if (transaction.OrderID17 == 0) return 17;
            else if (transaction.OrderID18 == 0) return 18;
            else if (transaction.OrderID19 == 0) return 19;
            else return 20;
        }
        public void ApplyOrderToTransactionSQL(FoodBevOrder foodBevOrderToInsert, int nullOrderSlot)
        {
            switch(nullOrderSlot)
            {
                case 1:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID1 = @oID, OrderPrice1 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 2:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID2 = @oID, OrderPrice2 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 3:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID3 = @oID, OrderPrice3 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 4:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID4 = @oID, OrderPrice4 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 5:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID5 = @oID, OrderPrice5 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 6:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID6 = @oID, OrderPrice6 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 7:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID7 = @oID, OrderPrice7 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 8:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID8 = @oID, OrderPrice8 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 9:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID9 = @oID, OrderPrice9 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 10:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID10 = @oID, OrderPrice10 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 11:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID11 = @oID, OrderPrice11 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 12:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID12 = @oID, OrderPrice12 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 13:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID13 = @oID, OrderPrice13 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 14:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID14 = @oID, OrderPrice14 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 15:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID15 = @oID, OrderPrice15 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 16:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID16 = @oID, OrderPrice16 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 17:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID17 = @oID, OrderPrice17 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 18:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID18 = @oID, OrderPrice18 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                case 19:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID19 = @oID, OrderPrice19 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
                default:
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET OrderID20 = @oID, OrderPrice20 = @pID WHERE SALESTRANSACTIONID = @tID;", new { oID = foodBevOrderToInsert.OrderID, pID = foodBevOrderToInsert.OrderSaleAmount, tID = foodBevOrderToInsert.TransactionID });
                    break;
            }
            
        }
        public IEnumerable<MenuItem> GetMenuItemListSQL()
        {
            return _conn.Query<MenuItem>("SELECT * FROM MENU_ITEMS;");
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
            return _conn.Query<Employee>("SELECT * FROM EMPLOYEES WHERE ROLE = 'SERVER';");
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
        public void FulfillFoodBevOrderSQL(FoodBevOrder foodBevOrderToFulfill)
        {
            _conn.Execute("UPDATE FOOD_BEV_ORDERS SET ORDERFULFILLED = @fulfilled WHERE ORDERID = @oID;",
               new { fulfilled = 1, oID = foodBevOrderToFulfill.OrderID });
        }

        public int GetServerSQL(int transactionID)
        {
            var transaction = _conn.QuerySingle<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = transactionID });
            return transaction.EmployeeID;
        }
        public int GetRestaurantTableSQL(int transactionID)
        {
            var transaction = _conn.QuerySingle<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = transactionID });
            return transaction.RestaurantTableID;
        }

        public FoodBevOrder CreateShellFoodBevOrderSQL()
        {
            FoodBevOrder order = new FoodBevOrder();

            var menuItemList = AssignMenuItemListSQL();
            order.MenuItemList = menuItemList.MenuItemList;

            var serverList = AssignServerListSQL();
            order.ServerList = serverList.ServerList;

            var tableList = AssignRestaurantTableListSQL();
            order.RestaurantTableList = tableList.RestaurantTableList;

            return order;
        }

        public decimal CalculateOrderPriceSQL(FoodBevOrder orderToCalculate)
        {
            var subTotalPerItemList = new List<decimal>();
            
            orderToCalculate.PriceItem1 = GetPerUnitPriceSQL(orderToCalculate.MenuItemName1);
            orderToCalculate.PriceItem2 = GetPerUnitPriceSQL(orderToCalculate.MenuItemName2);
            orderToCalculate.PriceItem3 = GetPerUnitPriceSQL(orderToCalculate.MenuItemName3);
            orderToCalculate.PriceItem4 = GetPerUnitPriceSQL(orderToCalculate.MenuItemName4);

            subTotalPerItemList.Add(orderToCalculate.PriceItem1 * orderToCalculate.QuantityItem1);
            subTotalPerItemList.Add(orderToCalculate.PriceItem2 * orderToCalculate.QuantityItem2);
            subTotalPerItemList.Add(orderToCalculate.PriceItem3 * orderToCalculate.QuantityItem3);
            subTotalPerItemList.Add(orderToCalculate.PriceItem4 * orderToCalculate.QuantityItem4);

            return subTotalPerItemList.Sum();
        }

        public decimal GetPerUnitPriceSQL(string menuItem)
        {
            if(menuItem != null)
            {
                var item = _conn.QuerySingle<MenuItem>("SELECT * FROM MENU_ITEMS WHERE MENUITEMNAME = @name;", new { name = menuItem });
                return item.MenuItemPrice;
            }
            return 0;
        }
    }
}
