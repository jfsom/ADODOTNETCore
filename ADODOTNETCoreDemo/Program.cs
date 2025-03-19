using Microsoft.Data.SqlClient;
using System.Data;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Connection String
            string connectionString = "Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
            //Creating an Instance of SqlConnection Object
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                //Open the Connection
                connection.Open();
                Console.WriteLine("Connection Established Successfully");

                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
            finally
            {
                // Ensure the connection is closed even if an exception occurs
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection is Closed");
                }
                connection.Dispose();
            }
        }
    }
}