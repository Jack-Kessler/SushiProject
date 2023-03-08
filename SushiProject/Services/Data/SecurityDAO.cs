using MySql.Data.MySqlClient;
using SushiProject.Models;
using System.Data.SqlClient;

namespace SushiProject.Services.Data
{
    public class SecurityDAO
    {
        //Connect to the database (i.e. UsernamesAndPasswords db)
        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=UsernamesAndPasswords;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        internal bool FindByUser(UserModel user)
        {
            // start by assuming that nothing is found in this query
            bool success = false;

            //write the sql expression
            string queryString = "SELECT * FROM dbo.Users WHERE username = @Username AND password = @Password";

            //create and open the connection to the database inside a using block.

            //This ensures that all resources are closded properly when the query is done.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create the command and parameter objects
                SqlCommand command = new SqlCommand(queryString, connection);

                //associate @Username with user.Username. & @Password with user.Password
                command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 50).Value = user.Username;

                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;

                //open the database and run the command.
                try 
                { 
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                } 
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return success;
            }
        }
    }
}
