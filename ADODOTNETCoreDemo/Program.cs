using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    // Step 1: Start a SQL transaction.
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Step 2: Create and Execute a SqlCommand
                            string cmdText = @"INSERT INTO Employee (FirstName, LastName, Email, Position, Salary) VALUES (@FirstName, @LastName, @Email, @Position, @Salary)";

                            using (SqlCommand command = new SqlCommand(cmdText, connection, transaction))
                            {
                                // Add parameters to prevent SQL injection
                                command.Parameters.AddWithValue("@FirstName", "Rakesh");
                                command.Parameters.AddWithValue("@LastName", "Sharma");
                                command.Parameters.AddWithValue("@Email", "Rakesh@Example.com");
                                command.Parameters.AddWithValue("@Position", "DBA");
                                command.Parameters.AddWithValue("@Salary", 10000);

                                // Execute the command
                                int result = command.ExecuteNonQuery();
                                Console.WriteLine("Rows affected: " + result);

                                // Step 3: Commit the Transaction
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An exception occurred. Transaction rolled back.");
                            Console.WriteLine(ex.Message);

                            // Rollback the transaction in case of an error
                            transaction.Rollback();
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
}