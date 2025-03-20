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
                //Establishing a Database Connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection to the database
                    connection.Open();

                    // Preparing the SQL Command
                    // Create the command with both SELECT statements
                    string query = "SELECT * FROM Customers; SELECT * FROM Orders;";

                    //Create the Command Object
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //Execute the Command using ExecuteReader which will return an instance of SqlDataReader
                        //SqlDataReader will have both result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // First result set (Customers), i.e., First Select Statement Result
                            Console.WriteLine("Customers:");

                            //The Read method on the SqlDataReader is called in a loop to read each row in the first result set (Customers).
                            while (reader.Read())
                            {
                                //For each row, it accesses column values (like CustomerID, Name, etc.) and prints them to the console.
                                Console.WriteLine($"\tID: {reader["CustomerID"]}, Name: {reader["Name"]}, Email: {reader["Email"]}, Address: {reader["Address"]}");
                            }

                            // Move to the next result set (Orders), i.e., Select Select Statement Result if any
                            // The reader.NextResult() is called to advance the reader to the next result set (Orders), if available.
                            if (reader.NextResult())
                            {
                                //Process is same to read the data from the second result set
                                Console.WriteLine("\nOrders:");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"\tOrderID: {reader["OrderID"]}, CustomerID: {reader["CustomerID"]}, OrderDate: {reader["OrderDate"]}, TotalAmount: {reader["TotalAmount"]}");
                                }
                            }
                        }
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}