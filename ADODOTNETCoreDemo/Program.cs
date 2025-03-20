using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Employee";
                    command.Connection = new SqlConnection(connectionString);
                    command.Connection.Open();

                    int count = (int)command.ExecuteScalar();

                    Console.WriteLine($"Total Employees: {count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}