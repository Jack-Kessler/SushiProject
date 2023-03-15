using SushiProject.Models;

namespace SushiProject
{
    public interface ISalesTransactionRepository
    {
        public IEnumerable<SalesTransaction> GetAllSalesTransactionsSQL();
        public SalesTransaction GetSalesTransactionSQL(int salesTransactionID);
        public void UpdateSalesTransactionSQL(SalesTransaction salesTransactionToUpdate);
        public void InsertSalesTransactionSQL(SalesTransaction salesTransactionToInsert);
        public IEnumerable<FoodBevOrder> GetOrderListSQL();
        public SalesTransaction AssignOrderListSQL();
        public IEnumerable<Employee> GetServerListSQL();
        public SalesTransaction AssignServerListSQL();
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL();
        public SalesTransaction AssignRestaurantTableListSQL();
        public void DeleteSalesTransactionSQL(SalesTransaction salesTransactionToDelete);
        public void CompleteSalesTransactionSQL(SalesTransaction salesTransactionToComplete);
        public SalesTransaction CreateShellSalesTransaction();
        public decimal CalculateTotalSalesTransactionAmount(SalesTransaction transactionToCalculate);

        public decimal GetPerOrderPrice(int orderID);
    }
}
