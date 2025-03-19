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
                // Example student data to insert
                string studentFirstName = "Pranaya";
                string studentLastName = "Rout";
                string studentEmail = "Pranaya@Example.com";

                // SQL INSERT statement
                string insertSql = "INSERT INTO Students (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)";

                //While Creating the SqlConnection passing the Connection String
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    connection.Open();

                    //Create the Command Objecr
                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@FirstName", studentFirstName);
                        command.Parameters.AddWithValue("@LastName", studentLastName);
                        command.Parameters.AddWithValue("@Email", studentEmail);

                        //Execute the Coomand
                        int result = command.ExecuteNonQuery();

                        // Check the result
                        if (result < 0)
                            Console.WriteLine("Error Inserting Data Into Database!");
                        else
                            Console.WriteLine("Data Inserted Successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
        }
    }
}