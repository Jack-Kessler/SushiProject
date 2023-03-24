using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Mysqlx.Crud;
using SushiProject.Models;
using System.Data;
using System.Transactions;

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
        public IEnumerable<SalesTransaction> GetInitialSalesTransactionSQL(SalesTransaction transaction)
        {
            return _conn.Query<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE EMPLOYEEID = @empID AND RestaurantTableID = @tableID AND SALESTRANSACTIONCOMPLETED = @com;", new { empID = transaction.EmployeeID, tableID = transaction.RestaurantTableID, com = 0 });
        }
        public SalesTransaction GetSalesTransactionSQL(int salesTransactionID)
        {
            return _conn.QuerySingle<SalesTransaction>("SELECT * FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = salesTransactionID });
        }
        public void UpdateSalesTransactionToDatabaseSQL(SalesTransaction salesTransactionToUpdate)
        {
            var transaction = GetSalesTransactionSQL(salesTransactionToUpdate.SalesTransactionID);

            if (transaction.SalesTransactionCompleted == false && salesTransactionToUpdate.SalesTransactionCompleted == true)
            {
                CompleteSalesTransactionSQL(salesTransactionToUpdate);
                //Essentially finalized transaction for firs time so need to use the method above.
            }
            else
            {
                if (transaction.SalesTransactionCompleted == true) //If the transaction was already finalized and editing after the fact
                {
                    var lastRecord = _conn.QuerySingle<MoneyAccounting>("SELECT * FROM MONEY_ACCOUNTING ORDER BY CREDITDEBITID DESC LIMIT 1;");
                    var totalAfterCredit = lastRecord.TotalBalance - transaction.FinalTransactionAmount;
                    var negative = transaction.FinalTransactionAmount * -1;
                    var totalAfterDebit = totalAfterCredit + salesTransactionToUpdate.FinalTransactionAmount;

                    _conn.Execute("INSERT INTO MONEY_ACCOUNTING (DEBITORCREDIT, DEBITCREDITTYPE, DEBITCREDITAMOUNT, SALESTRANSACTIONID, DATEANDTIME, TOTALBALANCE) VALUES(@or, @type, @amt, @sales, @date, @total);",
                    new { or = "CREDIT", type = "ALTERED TRANSACTION", amt = negative, sales = salesTransactionToUpdate.SalesTransactionID, date = DateTime.Now, total = totalAfterCredit });
                    //Creating entry to credit total of altered transaction (essentially voiding it from math perspective)

                    _conn.Execute("INSERT INTO MONEY_ACCOUNTING (DEBITORCREDIT, DEBITCREDITTYPE, DEBITCREDITAMOUNT, SALESTRANSACTIONID, DATEANDTIME, TOTALBALANCE) VALUES(@or, @type, @amt, @sales, @date, @total);",
                    new { or = "DEBIT", type = "ALTERED TRANSACTION", amt = salesTransactionToUpdate.FinalTransactionAmount, sales = salesTransactionToUpdate.SalesTransactionID, date = DateTime.Now, total = totalAfterDebit });
                    //Creating another entry to debit total of altered transaction (essentially adding back in whatever the correct final transaction amount is)
                }
                //Code below is for any transaction that was NOT closed initially but is now finalized with update
                _conn.Execute("UPDATE SALES_TRANSACTIONS SET SALESTRANSACTIONCOMPLETED = @complete, ALLYOUCANEAT = @eat, NUMOFCUSTOMERSADULT = @adult, NUMOFCUSTOMERSCHILD = @child, TIPAMOUNT = @tip, PAYMENTMETHOD = @pay, EMPLOYEEID = @eID, RESTAURANTTABLEID = @tID, " +
                  "ORDERID1 = @o1, " +
                  "ORDERID2 = @o2, " +
                  "ORDERID3 = @o3, " +
                  "ORDERID4 = @o4, " +
                  "ORDERID5 = @o5, " +
                  "ORDERID6 = @o6, " +
                  "ORDERID7 = @o7, " +
                  "ORDERID8 = @o8, " +
                  "ORDERID9 = @o9, " +
                  "ORDERID10 = @o10, " +
                  "ORDERID11 = @o11, " +
                  "ORDERID12 = @o12, " +
                  "ORDERID13 = @o13, " +
                  "ORDERID14 = @o14, " +
                  "ORDERID15 = @o15, " +
                  "ORDERID16 = @o16, " +
                  "ORDERID17 = @o17, " +
                  "ORDERID18 = @o18, " +
                  "ORDERID19 = @o19, " +
                  "ORDERID20 = @o20 " +
                  "WHERE SALESTRANSACTIONID = @ID;",
                 new
                 {
                     complete = salesTransactionToUpdate.SalesTransactionCompleted,
                     eat = salesTransactionToUpdate.AllYouCanEat,
                     adult = salesTransactionToUpdate.NumOfCustomersAdult,
                     child = salesTransactionToUpdate.NumOfCustomersChild,
                     tip = salesTransactionToUpdate.TipAmount,
                     pay = salesTransactionToUpdate.PaymentMethod,
                     eID = salesTransactionToUpdate.EmployeeID,
                     tID = salesTransactionToUpdate.RestaurantTableID,
                     o1 = salesTransactionToUpdate.OrderID1,
                     o2 = salesTransactionToUpdate.OrderID2,
                     o3 = salesTransactionToUpdate.OrderID3,
                     o4 = salesTransactionToUpdate.OrderID4,
                     o5 = salesTransactionToUpdate.OrderID5,
                     o6 = salesTransactionToUpdate.OrderID6,
                     o7 = salesTransactionToUpdate.OrderID7,
                     o8 = salesTransactionToUpdate.OrderID8,
                     o9 = salesTransactionToUpdate.OrderID9,
                     o10 = salesTransactionToUpdate.OrderID10,
                     o11 = salesTransactionToUpdate.OrderID11,
                     o12 = salesTransactionToUpdate.OrderID12,
                     o13 = salesTransactionToUpdate.OrderID13,
                     o14 = salesTransactionToUpdate.OrderID14,
                     o15 = salesTransactionToUpdate.OrderID15,
                     o16 = salesTransactionToUpdate.OrderID16,
                     o17 = salesTransactionToUpdate.OrderID17,
                     o18 = salesTransactionToUpdate.OrderID18,
                     o19 = salesTransactionToUpdate.OrderID19,
                     o20 = salesTransactionToUpdate.OrderID20,
                     ID = salesTransactionToUpdate.SalesTransactionID
                 });
            }
        }
        public void UpdateOrderPricesInDatabaseSQL(SalesTransaction transaction)
        {
            while (true)
            {
                if (transaction.OrderID1 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID1});
                    transaction.OrderPrice1 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE1 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice1, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID2 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID2 });
                    transaction.OrderPrice2 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE2 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice2, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID3 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID3 });
                    transaction.OrderPrice3 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE3 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice3, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID4 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID4 });
                    transaction.OrderPrice4 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE4 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice4, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID5 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID5 });
                    transaction.OrderPrice5 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE5 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice5, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID6 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID6 });
                    transaction.OrderPrice6 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE6 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice6, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID7 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID7 });
                    transaction.OrderPrice7 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE7 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice7, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID8 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID8 });
                    transaction.OrderPrice8 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE8 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice8, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID9 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID9 });
                    transaction.OrderPrice9 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE9 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice9, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID10 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID10 });
                    transaction.OrderPrice10 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE10 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice10, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID11 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID11 });
                    transaction.OrderPrice11 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE11 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice11, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID12 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID12 });
                    transaction.OrderPrice12 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE12 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice12, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID13 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID13 });
                    transaction.OrderPrice13 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE13 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice13, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID14 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID14 });
                    transaction.OrderPrice14 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE14 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice14, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID15 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID15 });
                    transaction.OrderPrice15 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE15 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice15, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID16 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID16 });
                    transaction.OrderPrice16 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE16 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice16, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID17 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID17 });
                    transaction.OrderPrice17 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE17 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice17, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID18 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID18 });
                    transaction.OrderPrice18 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE18 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice18, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID19 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID19 });
                    transaction.OrderPrice19 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE19 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice19, ID = transaction.SalesTransactionID });
                }
                else break;
                if (transaction.OrderID20 != null)
                {
                    var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @ID;", new { ID = transaction.OrderID20 });
                    transaction.OrderPrice20 = order.OrderSaleAmount;
                    _conn.Execute("UPDATE SALES_TRANSACTIONS SET ORDERPRICE20 = @price WHERE SALESTRANSACTIONID = @ID;", new { price = transaction.OrderPrice20, ID = transaction.SalesTransactionID });
                }
                else break;
                break;
            }
        }
        public void InsertSalesTransactionSQL(SalesTransaction salesTransactionToInsert)
        {
            _conn.Execute("INSERT INTO SALES_TRANSACTIONS (ALLYOUCANEAT, NUMOFCUSTOMERSADULT, NUMOFCUSTOMERSCHILD, TAXRATEFRACTIONALEQUIVALENT, FINALTRANSACTIONDATEANDTIME, EMPLOYEEID, RESTAURANTTABLEID, ORDERID1, ORDERPRICE1, ORDERID2, ORDERPRICE2, ORDERID3, ORDERPRICE3, ORDERID4, ORDERPRICE4, ORDERID5, ORDERPRICE5, ORDERID6, ORDERPRICE6, ORDERID7, ORDERPRICE7, ORDERID8, ORDERPRICE8, ORDERID9, ORDERPRICE9, ORDERID10, ORDERPRICE10, ORDERID11, ORDERPRICE11, ORDERID12, ORDERPRICE12, ORDERID13, ORDERPRICE13, ORDERID14, ORDERPRICE14, ORDERID15, ORDERPRICE15, ORDERID16, ORDERPRICE16, ORDERID17, ORDERPRICE17, ORDERID18, ORDERPRICE18, ORDERID19, ORDERPRICE19, ORDERID20, ORDERPRICE20) VALUES(@eat, @adult, @child, @tax, @date, @eID, @tID, @o1, @p1, @o2, @p2, @o3, @p3, @o4, @p4, @o5, @p5, @o6, @p6, @o7, @p7, @o8, @p8, @o9, @p9, @o10, @p10, @o11, @p11, @o12, @p12, @o13, @p13, @o14, @p14, @o15, @p15, @o16, @p16, @o17, @p17, @o18, @p18, @o19, @p19, @o20, @p20);",
           new
           {
               eat = salesTransactionToInsert.AllYouCanEat,
               adult = salesTransactionToInsert.NumOfCustomersAdult,
               child = salesTransactionToInsert.NumOfCustomersChild,
               tax = salesTransactionToInsert.TaxRateFractionalEquivalent,
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
        public IEnumerable<Employee> GetServerListSQL()
        {
            return _conn.Query<Employee>("SELECT * FROM EMPLOYEES WHERE ROLE = 'SERVER';");
        }
        public SalesTransaction AssignServerListSQL()
        {
            var serverList = GetServerListSQL();
            var salesTransaction = new SalesTransaction();
            salesTransaction.ServerList = serverList;
            return salesTransaction;
        }
        public IEnumerable<RestaurantTable> GetAllRestaurantTableListSQL()
        {
            return _conn.Query<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES;");
        }
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL(int id)
        {
            return _conn.Query<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES WHERE RestaurantTableAssignedEmployeeID = @id;", new { id = id });
        }
        public SalesTransaction AssignRestaurantTableListSQL(int employeeID)
        {
            var tableList = GetRestaurantTableListSQL(employeeID);
            var salesTransaction = new SalesTransaction();
            salesTransaction.RestaurantTableList = tableList;
            return salesTransaction;
        }
        public void DeleteSalesTransactionSQL(SalesTransaction salesTransactionToDelete)
        {
            var transaction = GetSalesTransactionSQL(salesTransactionToDelete.SalesTransactionID);
            var debitAmt = transaction.FinalTransactionAmount * -1;
            var lastRecord = _conn.QuerySingle<MoneyAccounting>("SELECT * FROM MONEY_ACCOUNTING ORDER BY CREDITDEBITID DESC LIMIT 1;");
            var newTotal = lastRecord.TotalBalance + debitAmt;

            _conn.Execute("DELETE FROM SALES_TRANSACTIONS WHERE SALESTRANSACTIONID = @id;", new { id = salesTransactionToDelete.SalesTransactionID });
            _conn.Execute("INSERT INTO MONEY_ACCOUNTING (DEBITORCREDIT, DEBITCREDITTYPE, DEBITCREDITAMOUNT, SALESTRANSACTIONID, DATEANDTIME, TOTALBALANCE) VALUES(@or, @type, @amt, @sales, @date, @total);",
            new { or = "CREDIT", type = "VOID TRANSACTION", amt = debitAmt, sales = salesTransactionToDelete.SalesTransactionID, date = DateTime.Now, total = newTotal });

        }
        public void CompleteSalesTransactionSQL(SalesTransaction salesTransactionToComplete)
        {
            salesTransactionToComplete.FinalTransactionAmount = (decimal)(salesTransactionToComplete.SubTotalAfterTax + salesTransactionToComplete.TipAmount);
            salesTransactionToComplete.FinalTransactionDateAndTime = DateTime.Now;
            _conn.Execute("UPDATE SALES_TRANSACTIONS SET SALESTRANSACTIONCOMPLETED = @complete, PAYMENTMETHOD = @pay, TIPAMOUNT = @tip, FinalTRANSACTIONAMOUNT = @finalamt, FinalTransactionDateAndTime = @date WHERE SALESTRANSACTIONID = @id;",
            new { complete = 1, pay =salesTransactionToComplete.PaymentMethod, tip = salesTransactionToComplete.TipAmount, finalamt = salesTransactionToComplete.FinalTransactionAmount, date =salesTransactionToComplete.FinalTransactionDateAndTime, id = salesTransactionToComplete.SalesTransactionID });

            var lastRecord = _conn.QuerySingle<MoneyAccounting>("SELECT * FROM MONEY_ACCOUNTING ORDER BY CREDITDEBITID DESC LIMIT 1;");
            var newTotal = lastRecord.TotalBalance + salesTransactionToComplete.FinalTransactionAmount;

            _conn.Execute("INSERT INTO MONEY_ACCOUNTING (DEBITORCREDIT, DEBITCREDITTYPE, DEBITCREDITAMOUNT, SALESTRANSACTIONID, DATEANDTIME, TOTALBALANCE) VALUES(@or, @type, @amt, @id, @date, @total);",
            new { or = "DEBIT", type = salesTransactionToComplete.PaymentMethod, amt = salesTransactionToComplete.FinalTransactionAmount, id = salesTransactionToComplete.SalesTransactionID, date = salesTransactionToComplete.FinalTransactionDateAndTime, total = newTotal});
        }
        public bool CheckCustomerLogoutPasswordSQL(string enteredPass)
        {
            var actualPass = _conn.QuerySingle<CustomerLogoutPassword>("SELECT * FROM CUSTOMER_LOGOUT_PASSWORD WHERE CUSTOMERLOGOUTPASSWORDID = 1;");
            string currentPassword = actualPass.CurrentPassword;
            return currentPassword == enteredPass ? true : false;
        }

        public string GetLogoutPasswordSQL()
        {
            var actualPass = _conn.QuerySingle<CustomerLogoutPassword>("SELECT * FROM CUSTOMER_LOGOUT_PASSWORD WHERE CUSTOMERLOGOUTPASSWORDID = 1;");
            return actualPass.CurrentPassword;
        }
        public SalesTransaction CreateShellSalesTransactionSQL(SalesTransaction transaction)
        {
            var currentTaxRate = GetTaxRateSQL();
            transaction.TaxRateFractionalEquivalent = (decimal)currentTaxRate.CurrentTaxRate; //Cast added because tax rate is nullable

            var tableList = AssignRestaurantTableListSQL(transaction.EmployeeID);
            transaction.RestaurantTableList = tableList.RestaurantTableList;

            transaction.FinalTransactionAmount = 0; //Set to 0 for time being to satisfy "ModelIsValid" in calling method.

            return transaction;
        }
        public TaxRate GetTaxRateSQL()
        {
            var tax = _conn.QuerySingle<TaxRate>("SELECT * FROM TAX_RATE WHERE TAXRATEID = 1;");
            return tax;
        }
        public decimal GetPerOrderPriceSQL(int orderID)
        {
            if (orderID != 0)
            {
                var order = _conn.QuerySingle<FoodBevOrder>("SELECT * FROM FOOD_BEV_ORDERS WHERE ORDERID = @id;", new { id = orderID });
                return order.OrderSaleAmount;
            }
            return 0;
        }
        public SalesTransaction CalculateSubTotalAmountSQL(int transactionID)
        {
            var transaction = GetSalesTransactionSQL(transactionID);
            if (transaction.AllYouCanEat == true)
            {
                var adultRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 1");
                var childRate = _conn.QuerySingle<AllYouCanEat>("SELECT * FROM ALL_YOU_CAN_EAT WHERE ALLYOUCANEATID = 2");

                transaction.SubTotalPreTax = (decimal)((adultRate.AllYouCanEatRate * transaction.NumOfCustomersAdult) + (childRate.AllYouCanEatRate * transaction.NumOfCustomersChild));
                //needed to add case because NumOfCustomersAdult and NumOfCustomersChild are nullable - which is required for ModelState.IsValid to work

                transaction.TaxAmount = Math.Round((transaction.SubTotalPreTax * transaction.TaxRateFractionalEquivalent), 2);
                transaction.SubTotalAfterTax = transaction.SubTotalPreTax + transaction.TaxAmount;
            }
            else
            {
                SalesTransaction tempTransaction = SetNullOrderPriceToZeroSQL(transaction);

                transaction.SubTotalPreTax = (decimal)(tempTransaction.OrderPrice1 + tempTransaction.OrderPrice2 + tempTransaction.OrderPrice3 + tempTransaction.OrderPrice4 + tempTransaction.OrderPrice5 + tempTransaction.OrderPrice6 + tempTransaction.OrderPrice7 + tempTransaction.OrderPrice8 + tempTransaction.OrderPrice9 + tempTransaction.OrderPrice10 + tempTransaction.OrderPrice11 + tempTransaction.OrderPrice12 + tempTransaction.OrderPrice13 + tempTransaction.OrderPrice14 + tempTransaction.OrderPrice15 + tempTransaction.OrderPrice16 + tempTransaction.OrderPrice17 + tempTransaction.OrderPrice18 + tempTransaction.OrderPrice19 + tempTransaction.OrderPrice20);

                transaction.TaxAmount = Math.Round((transaction.SubTotalPreTax * transaction.TaxRateFractionalEquivalent),2);

                transaction.SubTotalAfterTax = transaction.SubTotalPreTax + transaction.TaxAmount;  
            }

            _conn.Execute("UPDATE SALES_TRANSACTIONS SET SUBTOTALPRETAX = @pre, TAXAMOUNT = @tax, SUBTOTALAFTERTAX = @after WHERE SALESTRANSACTIONID = @ID;", new { ID = transactionID, pre = transaction.SubTotalPreTax, tax = transaction.TaxAmount, after = transaction.SubTotalAfterTax });

            transaction = SetBackToNullSQL(transaction);
            return transaction;
        }
        public SalesTransaction SetNullOrderPriceToZeroSQL(SalesTransaction transaction)
        {
            if (transaction.OrderPrice2 == null) { transaction.OrderPrice2 = 0; }
            if (transaction.OrderPrice3 == null) { transaction.OrderPrice3 = 0; }
            if (transaction.OrderPrice4 == null) { transaction.OrderPrice4 = 0; }
            if (transaction.OrderPrice5 == null) { transaction.OrderPrice5 = 0; }
            if (transaction.OrderPrice6 == null) { transaction.OrderPrice6 = 0; }
            if (transaction.OrderPrice7 == null) { transaction.OrderPrice7 = 0; }
            if (transaction.OrderPrice8 == null) { transaction.OrderPrice8 = 0; }
            if (transaction.OrderPrice9 == null) { transaction.OrderPrice9 = 0; }
            if (transaction.OrderPrice10 == null) { transaction.OrderPrice10 = 0; }
            if (transaction.OrderPrice11 == null) { transaction.OrderPrice11 = 0; }
            if (transaction.OrderPrice12 == null) { transaction.OrderPrice12 = 0; }
            if (transaction.OrderPrice13 == null) { transaction.OrderPrice13 = 0; }
            if (transaction.OrderPrice14 == null) { transaction.OrderPrice14 = 0; }
            if (transaction.OrderPrice15 == null) { transaction.OrderPrice15 = 0; }
            if (transaction.OrderPrice16 == null) { transaction.OrderPrice16 = 0; }
            if (transaction.OrderPrice17 == null) { transaction.OrderPrice17 = 0; }
            if (transaction.OrderPrice18 == null) { transaction.OrderPrice18 = 0; }
            if (transaction.OrderPrice19 == null) { transaction.OrderPrice19 = 0; }
            if (transaction.OrderPrice20 == null) { transaction.OrderPrice20 = 0; }
            return transaction;
        }
        public SalesTransaction SetBackToNullSQL(SalesTransaction transaction)
        {
            if (transaction.OrderPrice2 == 0) { transaction.OrderPrice2 = null; }
            if (transaction.OrderPrice3 == 0) { transaction.OrderPrice3 = null; }
            if (transaction.OrderPrice4 == 0) { transaction.OrderPrice4 = null; }
            if (transaction.OrderPrice5 == 0) { transaction.OrderPrice5 = null; }
            if (transaction.OrderPrice6 == 0) { transaction.OrderPrice6 = null; }
            if (transaction.OrderPrice7 == 0) { transaction.OrderPrice7 = null; }
            if (transaction.OrderPrice8 == 0) { transaction.OrderPrice8 = null; }
            if (transaction.OrderPrice9 == 0) { transaction.OrderPrice9 = null; }
            if (transaction.OrderPrice10 == 0) { transaction.OrderPrice10 = null; }
            if (transaction.OrderPrice11 == 0) { transaction.OrderPrice11 = null; }
            if (transaction.OrderPrice12 == 0) { transaction.OrderPrice12 = null; }
            if (transaction.OrderPrice13 == 0) { transaction.OrderPrice13 = null; }
            if (transaction.OrderPrice14 == 0) { transaction.OrderPrice14 = null; }
            if (transaction.OrderPrice15 == 0) { transaction.OrderPrice15 = null; }
            if (transaction.OrderPrice16 == 0) { transaction.OrderPrice16 = null; }
            if (transaction.OrderPrice17 == 0) { transaction.OrderPrice17 = null; }
            if (transaction.OrderPrice18 == 0) { transaction.OrderPrice18 = null; }
            if (transaction.OrderPrice19 == 0) { transaction.OrderPrice19 = null; }
            if (transaction.OrderPrice20 == 0) { transaction.OrderPrice20 = null; }
            return transaction;
        }
        public void CalculateFinalTransactionAmountSQL(SalesTransaction transaction)
        {
            transaction.FinalTransactionAmount = (decimal)(transaction.SubTotalAfterTax + transaction.TipAmount);
            transaction.FinalTransactionDateAndTime = DateTime.Now;

            _conn.Execute("UPDATE SALES_TRANSACTIONS SET FinalTRANSACTIONAMOUNT = @amt, FinalTransactionDateAndTime = @date WHERE SALESTRANSACTIONID = @id;",
            new {amt = transaction.FinalTransactionAmount, date = transaction.FinalTransactionDateAndTime, id = transaction.SalesTransactionID });
        }
        public IEnumerable<PaymentMethodCategory> GetPaymentMethodsListSQL()
        {
            var paymentMethodsList = _conn.Query<PaymentMethodCategory>("SELECT * FROM PAYMENT_METHOD_CATEGORIES");
            return paymentMethodsList;
        }
    }
}
