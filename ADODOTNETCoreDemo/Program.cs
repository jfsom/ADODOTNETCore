using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-RUC57UF;Database=OrderDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection to the database
                    connection.Open();

                    // Modify the query to join Customers and Orders tables
                    string query = @"SELECT c.CustomerID, c.Name, c.Email, c.Address, 
                                    o.OrderID, o.OrderDate, o.TotalAmount 
                                    FROM Customers c 
                                    JOIN Orders o ON c.CustomerID = o.CustomerID;";

                    //Create the Command Object
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //SqlDataReader will have both result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Customer and Orders:");
                            while (reader.Read())
                            {
                                // Assuming you are okay with repeating customer info for each order
                                Console.WriteLine($"CustomerID: {reader["CustomerID"]}, Name: {reader["Name"]}, Email: {reader["Email"]}, Address: {reader["Address"]}");
                                Console.WriteLine($"\t OrderID: {reader["OrderID"]}, OrderDate: {reader["OrderDate"]}, TotalAmount: {reader["TotalAmount"]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}