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
                string sqlQuery = "SELECT COUNT(*) FROM Employee";

                using (SqlCommand command = new SqlCommand(sqlQuery))
                {
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