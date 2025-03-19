using Microsoft.Data.SqlClient;

namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create the Connection String
                // string connectionString = "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;";

                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
                //While Creating the SqlConnection passing the Connection String
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    connection.Open();

                    //Create the Command Text
                    string createTableCommandText = @"
                    CREATE TABLE Students (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        FirstName NVARCHAR(50),
                        LastName NVARCHAR(50),
                        Email NVARCHAR(50)
                    );";

                    //Create an instance of the SqlCommand object by using the Command text and Connection object
                    using (SqlCommand command = new SqlCommand(createTableCommandText, connection))
                    {
                        //Call ExecuteNonQuery Method to Execute the command in the Provided Connection
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table 'Students' Created Successfully.");
                    }

                    //Close the Connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
        }
    }
}