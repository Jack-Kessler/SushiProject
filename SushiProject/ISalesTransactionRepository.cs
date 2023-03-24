using SushiProject.Models;
using System.Data.Common;
using System.Drawing;
using System.Transactions;

namespace SushiProject
{
    public interface ISalesTransactionRepository
    {
        public IEnumerable<SalesTransaction> GetAllSalesTransactionsSQL();
        public IEnumerable<SalesTransaction> GetInitialSalesTransactionSQL(SalesTransaction transaction);
        public SalesTransaction GetSalesTransactionSQL(int salesTransactionID);
        public void UpdateSalesTransactionToDatabaseSQL(SalesTransaction salesTransactionToUpdate);
        public void UpdateOrderPricesInDatabaseSQL(SalesTransaction transaction);
        public void InsertSalesTransactionSQL(SalesTransaction salesTransactionToInsert);
        public IEnumerable<FoodBevOrder> GetOrderListSQL();
        //public SalesTransaction AssignOrderListSQL();
        public IEnumerable<Employee> GetServerListSQL();
        public SalesTransaction AssignServerListSQL();
        public IEnumerable<RestaurantTable> GetAllRestaurantTableListSQL();
        //public SalesTransaction AssignAllRestaurantTableListSQL();
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL(int id);
        public SalesTransaction AssignRestaurantTableListSQL(int employeeID);
        public void DeleteSalesTransactionSQL(SalesTransaction salesTransactionToDelete);
        public void CompleteSalesTransactionSQL(SalesTransaction salesTransactionToComplete);
        public bool CheckCustomerLogoutPasswordSQL(string enteredPass);
        public SalesTransaction CreateShellSalesTransactionSQL(SalesTransaction transaction);
        public TaxRate GetTaxRateSQL();
        public decimal GetPerOrderPriceSQL(int orderID);
        public SalesTransaction CalculateSubTotalAmountSQL(int transactionID);
        public SalesTransaction SetNullOrderPriceToZeroSQL(SalesTransaction transaction);
        public SalesTransaction SetBackToNullSQL(SalesTransaction transaction);
        public void CalculateFinalTransactionAmountSQL(SalesTransaction transaction);
        public IEnumerable<PaymentMethodCategory> GetPaymentMethodsListSQL();
    }
}
