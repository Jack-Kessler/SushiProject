using Dapper;
using Mysqlx.Crud;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class RestaurantTableRepository : IRestaurantTableRepository
    {
        private readonly IDbConnection _conn;

        public RestaurantTableRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<RestaurantTable> GetAllRestaurantTablesSQL()
        {
            return _conn.Query<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES;");
        }

        public RestaurantTable GetRestaurantTableSQL(int restaurantTableID)
        {
            return _conn.QuerySingle<RestaurantTable>("SELECT * FROM RESTAURANT_TABLES WHERE RESTAURANTTABLEID = @id;", new { id = restaurantTableID });
        }

        public void UpdateRestaurantTableSQL(RestaurantTable restaurantTableToUpdate)
        {
            _conn.Execute("UPDATE RESTAURANT_TABLES SET RESTAURANTTABLEASSIGNEDEMPLOYEEID = @employeeID, RestaurantTableAssignedEmployeeFirstName = @fname, RestaurantTableAssignedEmployeeLastName = @lname WHERE RESTAURANTTABLEID = @tableID;",
                           new { employeeID = restaurantTableToUpdate.RestaurantTableAssignedEmployeeID, fname = restaurantTableToUpdate.RestaurantTableAssignedEmployeeFirstName, lname = restaurantTableToUpdate.RestaurantTableAssignedEmployeeLastName, tableID = restaurantTableToUpdate.RestaurantTableID });
        }

        public void InsertRestaurantTableSQL(RestaurantTable restaurantTableToInsert)
        {
            _conn.Execute("INSERT INTO RESTAURANT_TABLES(RESTAURANTTABLEASSIGNEDEMPLOYEEID) VALUES(null);");
        }
        public IEnumerable<Employee> GetServerListSQL()
        {
            return _conn.Query<Employee>("Select * FROM EMPLOYEES WHERE ROLE = \"Server\";");
        }
        public RestaurantTable ServerListForTableSQL()
        {
            var serverList = GetServerListSQL();
            var table = new RestaurantTable();
            table.ServerList = serverList;
            return table;
        }

        public void DeleteRestaurantTableSQL(RestaurantTable restaurantTableToDelete)
        {
            _conn.Execute("DELETE FROM RESTAURANT_TABLES WHERE RESTAURANTTABLEID = @id;", new { id = restaurantTableToDelete.RestaurantTableID });
        }
    }
}
