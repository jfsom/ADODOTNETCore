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
                // SQL query to delete a record
                string sqlQuery = "DELETE FROM Students WHERE Id = @Id";

                //While Creating the SqlConnection passing the Connection String
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the Connection
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Replace @Id with the actual id of the record you want to delete
                        command.Parameters.AddWithValue("@Id", 1);

                        //Execute the Delete Query
                        int result = command.ExecuteNonQuery();

                        // Check if the delete operation was successful
                        if (result > 0)
                        {
                            Console.WriteLine("Record Deleted Successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No Record Found with the Specified Id.");
                        }
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