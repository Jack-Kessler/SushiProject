using SushiProject.Models;
using System.Data;
using System.Transactions;

namespace SushiProject
{
    public interface IFoodBevOrderRepository
    {
        public IEnumerable<FoodBevOrder> GetAllFoodBevOrdersSQL();
        public FoodBevOrder GetFoodBevOrderSQL(int foodBevOrderID);
        public IEnumerable<FoodBevOrder> GetCustomerFoodBevOrdersSQL(int transactionID);
        public IEnumerable<FoodBevOrder> GetAllOpenFoodBevOrdersSQL();
        public void UpdateFoodBevOrderSQL(FoodBevOrder foodBevOrderToUpdate);
        public FoodBevOrder CleanUpInvalidMenuItemsSQL(FoodBevOrder foodBevOrderToInsert);
        public void InsertFoodBevOrderSQL(FoodBevOrder foodBevOrderToInsert);
        public int RetrieveOrderNumSQL(int transactionID, DateTime dateAndTime);
        public int FindFirstNullOrderSlotSQL(int transactionID);
        public void ApplyOrderToTransactionSQL(FoodBevOrder foodBevOrderToInsert, int nullOrderSlot);
        public IEnumerable<MenuItem> GetMenuItemListSQL();
        public FoodBevOrder AssignMenuItemListSQL();
        public IEnumerable<Employee> GetServerListSQL();
        public FoodBevOrder AssignServerListSQL();
        public IEnumerable<RestaurantTable> GetRestaurantTableListSQL();
        public FoodBevOrder AssignRestaurantTableListSQL();
        public void DeleteFoodBevOrderSQL(FoodBevOrder foodBevOrderToDelete);
        public void FulfillFoodBevOrderSQL(FoodBevOrder foodBevOrderToFulfill);
        public int GetServerSQL(int transactionID);
        public int GetRestaurantTableSQL(int transactionID);
        public FoodBevOrder CreateShellFoodBevOrderSQL();
        public decimal CalculateOrderPriceSQL(FoodBevOrder orderToCalculate);
        public decimal GetPerUnitPriceSQL(string menuItem);
        public void SubtractIngredientInventorySQL(FoodBevOrder order);
        public void SubtractIndividualIngredientFromInventorySQL(string item, int quantity);
    }
}