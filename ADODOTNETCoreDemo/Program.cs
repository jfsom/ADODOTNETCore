using Microsoft.Data.SqlClient;
//Step1: Import the following Namespace
using Microsoft.Extensions.Configuration;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Get the Connection String from appsettings.json file

                //Step2: Load the Configuration File.
                var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                // Step3: Get the Section to Read from the Configuration File
                var configSection = configBuilder.GetSection("ConnectionStrings");

                // Step4: Get the Configuration Values based on the Config key.
                var connectionString = configSection["SQLServerConnection"] ?? null;

                if (connectionString != null)
                {
                    //Creating an Instance of SqlConnection using the using statement
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        //Open the Connection
                        connection.Open();
                        Console.WriteLine("Connection Established Successfully");

                        //Perform the Database Operations
                    }
                }
                else
                {
                    Console.WriteLine("Connection String is Missing");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
        }
    }
}