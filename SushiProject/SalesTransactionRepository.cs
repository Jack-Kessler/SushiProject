using Dapper;
using Mysqlx.Crud;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class SalesTransactionRepository : ISalesTransactionRepository
    {
        private readonly IDbConnection _conn;

        public SalesTransactionRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<SalesTransaction> GetAllSalesTransactionsSQL()
        {
            return _conn.Query<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS;");
        }
        public SalesTransaction GetSalesTransactionSQL(int salesTransactionID)
        {
            return _conn.QuerySingle<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = salesTransactionID });
        }
        public void UpdateSalesTransactionSQL(SalesTransaction salesTransactionToUpdate)
        {
            _conn.Execute("UPDATE SALES_TRANSACTIONS SET FINALTRANSACTIONAMOUNT = @amt, FINALTRANSACTIONDATEANDTIME = @date, EMPLOYEEID = @eID, RESTAURANTTABLEID = @tID, " +
                "ORDERID1 = @o1, ORDERPRICE1 = @p1, " +
                "ORDERID2 = @o2, ORDERPRICE2 = @p2, " +
                "ORDERID3 = @o3, ORDERPRICE3 = @p3, " +
                "ORDERID4 = @o4, ORDERPRICE4 = @p4, " +
                "ORDERID5 = @o5, ORDERPRICE5 = @p5, " +
                "ORDERID6 = @o6, ORDERPRICE6 = @p6, " +
                "ORDERID7 = @o7, ORDERPRICE7 = @p7, " +
                "ORDERID8 = @o8, ORDERPRICE8 = @p8, " +
                "ORDERID9 = @o9, ORDERPRICE9 = @p9, " +
                "ORDERID10 = @o10, ORDERPRICE10 = @p10, " +
                "ORDERID11 = @o11, ORDERPRICE11 = @p11, " +
                "ORDERID12 = @o12, ORDERPRICE12 = @p12, " +
                "ORDERID13 = @o13, ORDERPRICE13 = @p13, " +
                "ORDERID14 = @o14, ORDERPRICE14 = @p14, " +
                "ORDERID15 = @o15, ORDERPRICE15 = @p15, " +
                "ORDERID16 = @o16, ORDERPRICE16 = @p16, " +
                "ORDERID17 = @o17, ORDERPRICE17 = @p17, " +
                "ORDERID18 = @o18, ORDERPRICE18 = @p18, " +
                "ORDERID19 = @o19, ORDERPRICE19 = @p19, " +
                "ORDERID20 = @o20, ORDERPRICE20 = @p20 " +
                "WHERE SALESTRANSACTIONID = @ID;",
               new { amt = salesTransactionToUpdate.FinalTransactionAmount, date =salesTransactionToUpdate.FinalTransactionDateAndTime, eID = salesTransactionToUpdate.EmployeeID, tID = salesTransactionToUpdate.RestaurantTableID, 
                   o1 = salesTransactionToUpdate.OrderID1, p1 = salesTransactionToUpdate.OrderPrice1,
                   o2 = salesTransactionToUpdate.OrderID2, p2 = salesTransactionToUpdate.OrderPrice2,
                   o3 = salesTransactionToUpdate.OrderID3, p3 = salesTransactionToUpdate.OrderPrice3,
                   o4 = salesTransactionToUpdate.OrderID4, p4 = salesTransactionToUpdate.OrderPrice4,
                   o5 = salesTransactionToUpdate.OrderID5, p5 = salesTransactionToUpdate.OrderPrice5,
                   o6 = salesTransactionToUpdate.OrderID6, p6 = salesTransactionToUpdate.OrderPrice6,
                   o7 = salesTransactionToUpdate.OrderID7, p7 = salesTransactionToUpdate.OrderPrice7,
                   o8 = salesTransactionToUpdate.OrderID8, p8 = salesTransactionToUpdate.OrderPrice8,
                   o9 = salesTransactionToUpdate.OrderID9, p9 = salesTransactionToUpdate.OrderPrice9,
                   o10 = salesTransactionToUpdate.OrderID10, p10 = salesTransactionToUpdate.OrderPrice10,
                   o11 = salesTransactionToUpdate.OrderID11, p11 = salesTransactionToUpdate.OrderPrice11,
                   o12 = salesTransactionToUpdate.OrderID12, p12 = salesTransactionToUpdate.OrderPrice12,
                   o13 = salesTransactionToUpdate.OrderID13, p13 = salesTransactionToUpdate.OrderPrice13,
                   o14 = salesTransactionToUpdate.OrderID14, p14 = salesTransactionToUpdate.OrderPrice14,
                   o15 = salesTransactionToUpdate.OrderID15, p15 = salesTransactionToUpdate.OrderPrice15,
                   o16 = salesTransactionToUpdate.OrderID16, p16 = salesTransactionToUpdate.OrderPrice16,
                   o17 = salesTransactionToUpdate.OrderID17, p17 = salesTransactionToUpdate.OrderPrice17,
                   o18 = salesTransactionToUpdate.OrderID18, p18 = salesTransactionToUpdate.OrderPrice18,
                   o19 = salesTransactionToUpdate.OrderID19, p19 = salesTransactionToUpdate.OrderPrice19,
                   o20 = salesTransactionToUpdate.OrderID20, p20 = salesTransactionToUpdate.OrderPrice20,
                   ID = salesTransactionToUpdate.SalesTransactionID });
        }
        public void InsertSalesTransactionSQL(SalesTransaction salesTransactionToInsert)
        {
            _conn.Execute("INSERT INTO SALES_TRANSACTIONS (FINALTRANSACTIONAMOUNT, FINALTRANSACTIONDATEANDTIME, EMPLOYEEID, RESTAURANTTABLEID, ORDERID1, ORDERPRICE1, ORDERID2, ORDERPRICE2, ORDERID3, ORDERPRICE3, ORDERID4, ORDERPRICE4, ORDERID5, ORDERPRICE5, ORDERID6, ORDERPRICE6, ORDERID7, ORDERPRICE7, ORDERID8, ORDERPRICE8, ORDERID9, ORDERPRICE9, ORDERID10, ORDERPRICE10, ORDERID11, ORDERPRICE11, ORDERID12, ORDERPRICE12, ORDERID13, ORDERPRICE13, ORDERID14, ORDERPRICE14, ORDERID15, ORDERPRICE15, ORDERID16, ORDERPRICE16, ORDERID17, ORDERPRICE17, ORDERID18, ORDERPRICE18, ORDERID19, ORDERPRICE19, ORDERID20, ORDERPRICE20) VALUES(@amt, @date, @eID, @tID, @o1, @p1, @o2, @p2, @o3, @p3, @o4, @p4, @o5, @p5, @o6, @p6, @o7, @p7, @o8, @p8, @o9, @p9, @o10, @p10, @o11, @p11, @o12, @p12, @o13, @p13, @o14, @p14, @o15, @p15, @o16, @p16, @o17, @p17, @o18, @p18, @o19, @p19, @o20, @p20);",
           new
           {
               amt = salesTransactionToInsert.FinalTransactionAmount,
               date = salesTransactionToInsert.FinalTransactionDateAndTime,
               eID = salesTransactionToInsert.EmployeeID,
               tID = salesTransactionToInsert.RestaurantTableID,
               o1 = salesTransactionToInsert.OrderID1,
               p1 = salesTransactionToInsert.OrderPrice1,
               o2 = salesTransactionToInsert.OrderID2,
               p2 = salesTransactionToInsert.OrderPrice2,
               o3 = salesTransactionToInsert.OrderID3,
               p3 = salesTransactionToInsert.OrderPrice3,
               o4 = salesTransactionToInsert.OrderID4,
               p4 = salesTransactionToInsert.OrderPrice4,
               o5 = salesTransactionToInsert.OrderID5,
               p5 = salesTransactionToInsert.OrderPrice5,
               o6 = salesTransactionToInsert.OrderID6,
               p6 = salesTransactionToInsert.OrderPrice6,
               o7 = salesTransactionToInsert.OrderID7,
               p7 = salesTransactionToInsert.OrderPrice7,
               o8 = salesTransactionToInsert.OrderID8,
               p8 = salesTransactionToInsert.OrderPrice8,
               o9 = salesTransactionToInsert.OrderID9,
               p9 = salesTransactionToInsert.OrderPrice9,
               o10 = salesTransactionToInsert.OrderID10,
               p10 = salesTransactionToInsert.OrderPrice10,
               o11 = salesTransactionToInsert.OrderID11,
               p11 = salesTransactionToInsert.OrderPrice11,
               o12 = salesTransactionToInsert.OrderID12,
               p12 = salesTransactionToInsert.OrderPrice12,
               o13 = salesTransactionToInsert.OrderID13,
               p13 = salesTransactionToInsert.OrderPrice13,
               o14 = salesTransactionToInsert.OrderID14,
               p14 = salesTransactionToInsert.OrderPrice14,
               o15 = salesTransactionToInsert.OrderID15,
               p15 = salesTransactionToInsert.OrderPrice15,
               o16 = salesTransactionToInsert.OrderID16,
               p16 = salesTransactionToInsert.OrderPrice16,
               o17 = salesTransactionToInsert.OrderID17,
               p17 = salesTransactionToInsert.OrderPrice17,
               o18 = salesTransactionToInsert.OrderID18,
               p18 = salesTransactionToInsert.OrderPrice18,
               o19 = salesTransactionToInsert.OrderID19,
               p19 = salesTransactionToInsert.OrderPrice19,
               o20 = salesTransactionToInsert.OrderID20,
               p20 = salesTransactionToInsert.OrderPrice20
           });
        }
        public IEnumerable<FoodBevOrder> GetOrderListSQL()
        {
            return _conn.Query<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS;");
        }
        public SalesTransaction AssignOrderListSQL()
        {
            var orderList = GetOrderListSQL();
            var salesTransaction = new SalesTransaction();
            salesTransaction.OrderList = orderList;
            return salesTransaction;
        }
        public IEnumerable<Employee> GetServerListSQL()
        {
            return _conn.Query<Employee>("SELECT * FROM EMPLOYEES;");
        }
        public SalesTransaction AssignServerListSQL()
        {
            var serverList = GetServerListSQL();
            var salesTransaction = new SalesTransaction();
            salesTransaction.ServerList = serverList;
            return salesTransaction;
        }
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL()
        {
            return _conn.Query<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES;");
        }
        public SalesTransaction AssignRestaurantTableListSQL()
        {
            var tableList = GetRestaurantTableListSQL();
            var salesTransaction = new SalesTransaction();
            salesTransaction.RestaurantTableList = tableList;
            return salesTransaction;
        }
        public void DeleteSalesTransactionSQL(SalesTransaction salesTransactionToDelete)
        {
            _conn.Execute("DELETE FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = salesTransactionToDelete.SalesTransactionID });
        }
        public void CompleteSalesTransactionSQL(SalesTransaction salesTransactionToComplete)
        {
            _conn.Execute("UPDATE SALES_TRANSACTIONS SET SALESTRANSACTIONCOMPLETED = @complete WHERE SALESTRANSACTIONID = @id;",
              new { complete = 1, id = salesTransactionToComplete.SalesTransactionID });
        }
        public SalesTransaction CreateShellSalesTransaction()
        {
            SalesTransaction sTransaction = new SalesTransaction();

            var orderList = AssignOrderListSQL();
            sTransaction.OrderList = orderList.OrderList;

            var serverList = AssignServerListSQL();
            sTransaction.ServerList = serverList.ServerList;

            var tableList = AssignRestaurantTableListSQL();
            sTransaction.RestaurantTableList = tableList.RestaurantTableList;

            return sTransaction;
        }
        public decimal CalculateTotalSalesTransactionAmount(SalesTransaction transactionToCalculate)
        {
            var subTotalPerOrderList = new List<decimal>();

            transactionToCalculate.OrderPrice1 = GetPerOrderPrice(transactionToCalculate.OrderID1);
            transactionToCalculate.OrderPrice2 = GetPerOrderPrice(transactionToCalculate.OrderID2);
            transactionToCalculate.OrderPrice3 = GetPerOrderPrice(transactionToCalculate.OrderID3);
            transactionToCalculate.OrderPrice4 = GetPerOrderPrice(transactionToCalculate.OrderID4);
            transactionToCalculate.OrderPrice5 = GetPerOrderPrice(transactionToCalculate.OrderID5);
            transactionToCalculate.OrderPrice6 = GetPerOrderPrice(transactionToCalculate.OrderID6);
            transactionToCalculate.OrderPrice7 = GetPerOrderPrice(transactionToCalculate.OrderID7);
            transactionToCalculate.OrderPrice8 = GetPerOrderPrice(transactionToCalculate.OrderID8);
            transactionToCalculate.OrderPrice9 = GetPerOrderPrice(transactionToCalculate.OrderID9);
            transactionToCalculate.OrderPrice10 = GetPerOrderPrice(transactionToCalculate.OrderID10);
            transactionToCalculate.OrderPrice11 = GetPerOrderPrice(transactionToCalculate.OrderID11);
            transactionToCalculate.OrderPrice12 = GetPerOrderPrice(transactionToCalculate.OrderID12);
            transactionToCalculate.OrderPrice13 = GetPerOrderPrice(transactionToCalculate.OrderID13);
            transactionToCalculate.OrderPrice14 = GetPerOrderPrice(transactionToCalculate.OrderID14);
            transactionToCalculate.OrderPrice15 = GetPerOrderPrice(transactionToCalculate.OrderID15);
            transactionToCalculate.OrderPrice16 = GetPerOrderPrice(transactionToCalculate.OrderID16);
            transactionToCalculate.OrderPrice17 = GetPerOrderPrice(transactionToCalculate.OrderID17);
            transactionToCalculate.OrderPrice18 = GetPerOrderPrice(transactionToCalculate.OrderID18);
            transactionToCalculate.OrderPrice19 = GetPerOrderPrice(transactionToCalculate.OrderID19);
            transactionToCalculate.OrderPrice20 = GetPerOrderPrice(transactionToCalculate.OrderID20);

            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice1);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice2);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice3);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice4);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice5);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice6);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice7);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice8);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice9);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice10);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice11);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice12);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice13);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice14);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice15);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice16);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice17);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice18);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice19);
            subTotalPerOrderList.Add(transactionToCalculate.OrderPrice20);

            return subTotalPerOrderList.Sum();
        }

        public decimal GetPerOrderPrice(int orderID)
        {
            if (orderID != 0)
            {
                var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @id;", new { id = orderID });
                return order.OrderSaleAmount;
            }
            return 0;
        }
    }
}
