using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //I am using Windows Authentication and hence no need to pass the User Id and Password
                string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

                // Query to Read All Employees Using ExecuteReader
                string readQuery = "SELECT * FROM Employee";

                // Query to Insert a New Employee using ExecuteNonQuery
                string insertQuery = "INSERT INTO Employee (FirstName, LastName, Email, Position, Salary) VALUES ('Ramesh', 'Sahoo', 'Ramesh@Example.com', 'HR Manager', 70000)";

                // Query to Get count of Employees using ExecuteScalar
                string countQuery = "SELECT COUNT(*) FROM Employee";

                //Create an Instance of SqlConnection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // ExecuteReaderAsync
                    Console.WriteLine("ExecuteReaderAsync Example");
                    using (SqlCommand command = new SqlCommand(readQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Email: {reader["Email"]}, Position: {reader["Position"]}, Salary: {reader["Salary"]}");
                            }
                        }
                    }

                    // ExecuteNonQueryAsync 
                    Console.WriteLine("\nExecuteNonQueryAsync Example");
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        int result = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"{result} row(s) Inserted");
                    }

                    // ExecuteScalarAsync 
                    Console.WriteLine("\nExecuteScalarAsync Example");
                    using (SqlCommand command = new SqlCommand(countQuery, connection))
                    {
                        int count = (int)await command.ExecuteScalarAsync();
                        Console.WriteLine($"Total Employees: {count}");
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}