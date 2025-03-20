using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //The connectionString contains information needed to establish a connection to the database,
            //including the server name, database name, and authentication method.
            string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

            //Creating an instance of SqlConnection. It's instantiated with the connection string.
            //Using block will ensures that the SQLConnection is disposed of correctly once it goes out of scope,
            //which closes the connection to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the Connection
                connection.Open();

                //A string containing the SQL query to be executed.
                //This query selects all columns from the Employees table.
                string sql = "SELECT EmployeeID, FirstName, LastName, Email, Department FROM Employees";

                //Represents an SQL statement that can be executed against an SQL Server database.
                //It is initialized with the SQL query and the connection object.
                SqlCommand command = new SqlCommand(sql, connection);

                //ExecuteReader Executes the SqlCommand and returns an SqlDataReader object to read the data returned by the query.
                //SqlDataReader read a forward-only stream of rows from a SQL Server database
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //Advances the SqlDataReader to the next record. It returns true if there are more rows; otherwise, it is false.
                    while (reader.Read())
                    {
                        //Accessed Data by column name
                        Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Email: {reader["Email"]}, Department: {reader["Department"]}");
                    }
                }
                Console.ReadLine();
            }
        }
    }
}