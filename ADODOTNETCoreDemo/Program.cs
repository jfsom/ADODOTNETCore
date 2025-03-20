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
                string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

                // Query to Insert a New Employee using ExecuteNonQuery
                string sqlQuery = @"INSERT INTO Employee (FirstName, LastName, Email, Position, Salary) VALUES (@FirstName, @LastName, @Email, @Position, @Salary)";

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // ExecuteNonQuery Example
                    Console.WriteLine("ExecuteNonQuery Example");
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Assuming these variables are set from your application's input
                        string firstName = "Priyanka";
                        string lastName = "Dewangan";
                        string email = "Priyanka@Example.com";
                        string position = "Software Developer";
                        decimal salary = 75000m;

                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Position", position);
                        command.Parameters.AddWithValue("@Salary", salary);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Employee Added Successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No Employee Was Added.");
                        }
                    } //Command Object will be disposed automatically

                } //Connection Object will be disposed automatically
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}