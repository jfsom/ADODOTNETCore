using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Connection String
                string connectionString = "Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
                //Creating an Instance of SqlConnection using the using statement
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    connection.Open();

                    //Use the Connection
                    Console.WriteLine("Connection Established Successfully");
                } //Automatically close the connection even if an exception is raised
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
        }
    }
}