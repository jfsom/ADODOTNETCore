using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
                //Using SqlConnection Constructor which takes connectionString as a parameter
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    connection.Open();

                    //Use the Connection
                    Console.WriteLine("Connection Established Successfully");
                } //Automatically using block close the connection even if exception has been occurred
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
        }
    }
}