using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

                // Query to Read All Employees Using ExecuteReader
                string readQuery = "SELECT * FROM Employee";

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // ExecuteReader Example
                    Console.WriteLine("ExecuteReader Example");
                    using (SqlCommand command = new SqlCommand(readQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Email: {reader["Email"]}, Position: {reader["Position"]}, Salary: {reader["Salary"]}");
                            }
                        }
                    } //Command Object will be disposed automatically
                } //Connection Object will be disposed automatically
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}