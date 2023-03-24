using SushiProject.Models;

namespace SushiProject
{
    public interface IRestaurantTableRepository
    {
        public IEnumerable<RestaurantTable> GetAllRestaurantTablesSQL();
        public RestaurantTable GetRestaurantTableSQL(int restaurantTableID);
        public void UpdateRestaurantTableSQL(RestaurantTable restaurantTableToUpdate);
        public void InsertRestaurantTableSQL(RestaurantTable restaurantTableToInsert);
        public IEnumerable<Employee> GetServerListSQL();
        public RestaurantTable ServerListForTableSQL();
        public void DeleteRestaurantTableSQL(RestaurantTable restaurantTableToDelete);
        public bool ReorderTableIDNumbersSQL();
    }
}
