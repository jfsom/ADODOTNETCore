using Microsoft.Data.SqlClient;
using System.Data;
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

                //Prepare the Query
                string selectQuery = "SELECT * FROM Employees";

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Create the SqlDataAdapter Object with the Select Query and Connection Object
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);

                    // Creating the commands that will be used by the SqlDataAdapter to update the database
                    SqlCommand updateCommand = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Department = @Department WHERE EmployeeID = @EmployeeID", connection);

                    // Adding parameters for the update command
                    updateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
                    updateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
                    updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
                    updateCommand.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");
                    updateCommand.Parameters.Add("@EmployeeID", SqlDbType.Int, 0, "EmployeeID");

                    dataAdapter.UpdateCommand = updateCommand;

                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with data from the database
                    dataAdapter.Fill(dataTable);

                    // Assuming you want to update the first row's Email for demonstration
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow rowToUpdate = dataTable.Rows[0];
                        rowToUpdate["Email"] = "updated.email@example1.com";
                    }

                    // Open the connection for the update
                    connection.Open();

                    // Perform the update on the database
                    int rowsAffected = dataAdapter.Update(dataTable);

                    // Close the connection
                    connection.Close();

                    Console.WriteLine($"{rowsAffected} row(s) were updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}