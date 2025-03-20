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

                // Asynchronously open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM Employees", connection);

                    DataTable dataTable = new DataTable();

                    // Execute the Fill method on a background thread
                    await Task.Run(() => dataAdapter.Fill(dataTable));

                    // Simulate an update operation on the DataTable (for demonstration)
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0]; // Example: modify the first row
                        row["Email"] = "updated.email@example.com"; // Modify the email column
                    }

                    // Prepare an SqlCommand for async execution (example for insert operation)
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, Department) VALUES (@FirstName, @LastName, @Email, @Department)", connection);
                    insertCommand.Parameters.AddWithValue("@FirstName", "Jane");
                    insertCommand.Parameters.AddWithValue("@LastName", "Doe");
                    insertCommand.Parameters.AddWithValue("@Email", "jane.doe@example.com");
                    insertCommand.Parameters.AddWithValue("@Department", "HR");

                    // Asynchronously execute the insert command
                    await insertCommand.ExecuteNonQueryAsync();

                    // Prepare the UpdateCommand for SqlDataAdapter to update the database based on changes in the DataTable
                    SqlCommand updateCommand = new SqlCommand("UPDATE Employees SET Email = @Email WHERE EmployeeID = @EmployeeID", connection);
                    updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
                    updateCommand.Parameters.Add("@EmployeeID", SqlDbType.Int, 4, "EmployeeID");
                    dataAdapter.UpdateCommand = updateCommand;

                    // Synchronously update the database with changes in the DataTable
                    // Note: There's no direct async equivalent for SqlDataAdapter.Update()
                    dataAdapter.Update(dataTable);

                    await connection.CloseAsync();
                    Console.WriteLine("Database operations have been completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}