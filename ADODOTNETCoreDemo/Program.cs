using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=OrderDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

                // Define the SQL query to be executed
                string query = "SELECT * FROM Customers";

                // Create and open a connection asynchronously
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    await connection.OpenAsync();

                    // Create a SqlCommand to execute the SQL query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the command and get a SqlDataReader asynchronously
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Read the data asynchronously
                            while (await reader.ReadAsync())
                            {
                                // Assuming Column1 is of type int and Column2 is of type string
                                int column1 = reader.GetInt32(0); //EmployeeID - Type INT
                                string column2 = reader.GetString(1); //FirstName - Type String
                                string column3 = reader.GetString(2); //LastName - Type String
                                string column4 = reader.GetString(3); //Email - Type String
                                //string column5 = reader.GetString(4); //Department - Type String

                                Console.WriteLine($"EmployeeID: {column1}, FirstName: {column2}, LastName: {column3}, Email: {column4}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}