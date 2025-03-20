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
                string query = "SELECT * FROM Employees";

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        //Create an Instance of SqlDataAdapter with the Query and Connection
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                        //Create a Data table
                        DataTable dataTable = new DataTable();

                        //Fill the Datatable using the Fill Method of the dataAdapter 
                        //The following things are done by the Fill method
                        //1. Open the connection
                        //2. Execute Command
                        //3. Retrieve the Result
                        //4. Fill/Store the Retrieve Result in the Data table
                        //5. Close the connection
                        dataAdapter.Fill(dataTable);

                        //Display the Data from the Data table
                        foreach (DataRow row in dataTable.Rows)
                        {
                            Console.WriteLine($"ID: {row["EmployeeID"]}, Name: {row["FirstName"]} {row["LastName"]}, Email: {row["Email"]}, Department: {row["Department"]}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
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