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

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();

                    // Setting up the SelectCommand
                    dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM Employees", connection);

                    // Setting up the InsertCommand
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, Department) VALUES (@FirstName, @LastName, @Email, @Department)", connection);
                    insertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
                    insertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
                    insertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
                    insertCommand.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");
                    dataAdapter.InsertCommand = insertCommand;

                    // Setting up the UpdateCommand
                    SqlCommand updateCommand = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Department = @Department WHERE EmployeeID = @EmployeeID", connection);
                    updateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
                    updateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
                    updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
                    updateCommand.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");
                    updateCommand.Parameters.Add("@EmployeeID", SqlDbType.Int, 0, "EmployeeID");
                    dataAdapter.UpdateCommand = updateCommand;

                    // Setting up the DeleteCommand
                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", connection);
                    deleteCommand.Parameters.Add("@EmployeeID", SqlDbType.Int, 0, "EmployeeID");
                    dataAdapter.DeleteCommand = deleteCommand;

                    DataTable dataTable = new DataTable();
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey; // Manage schema actions
                    dataAdapter.FillSchema(dataTable, SchemaType.Source); // Ensure schema is correct

                    // Fill the DataTable with current data
                    dataAdapter.Fill(dataTable);

                    // Demonstrating AcceptChangesDuringFill
                    dataAdapter.AcceptChangesDuringFill = false;

                    // Insert a new row (example)
                    DataRow newRow = dataTable.NewRow();
                    newRow["FirstName"] = "New";
                    newRow["LastName"] = "Employee";
                    newRow["Email"] = "new.employee@example.com";
                    newRow["Department"] = "IT";
                    dataTable.Rows.Add(newRow);

                    // Update an existing row (example)
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow rowToUpdate = dataTable.Rows[0];
                        rowToUpdate["Email"] = "updated.email@example.com";
                    }

                    // Delete a row (example)
                    if (dataTable.Rows.Count > 1)
                    {
                        dataTable.Rows[1].Delete();
                    }

                    // Handling errors during update
                    dataAdapter.ContinueUpdateOnError = true;

                    // Update the database
                    connection.Open();
                    dataAdapter.Update(dataTable);
                    connection.Close();

                    // Demonstrating AcceptChangesDuringUpdate
                    dataAdapter.AcceptChangesDuringUpdate = true;

                    // Check for errors
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row.HasErrors)
                        {
                            Console.WriteLine($"Row error: {row.RowError}");
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