using Microsoft.Data.SqlClient;

namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the Connection String
            // string connectionString = "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;";

            //I am using Windows Authentication and hence no need to pass the User Id and Password
            string connectionString = "Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Open the Connection
                connection.Open();
                Console.WriteLine("Successfully connected to the database.");

                connection.Close();
            }
        }
    }
}