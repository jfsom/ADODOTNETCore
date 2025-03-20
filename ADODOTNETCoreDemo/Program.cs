using Microsoft.Data.SqlClient;
using System.Data;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SqlCommand for the stored procedure
                    SqlCommand command = new SqlCommand("GetAllEmployees", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Create the SqlDataAdapter with the SqlCommand
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    // Create a DataTable to hold the retrieved data
                    DataTable dataTable = new DataTable();

                    // Open the connection and fill the DataTable
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    connection.Close();

                    // Display the data (for demonstration purposes)
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine($"ID: {row["EmployeeID"]}, Name: {row["FirstName"]} {row["LastName"]}, Email: {row["Email"]}, Department: {row["Department"]}");
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