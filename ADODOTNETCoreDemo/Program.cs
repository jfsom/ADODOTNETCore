using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-RUC57UF;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT EmployeeID, FirstName, LastName, Email FROM Employees";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"HasRows: {reader.HasRows}");

                    Console.WriteLine($"FieldCount: {reader.FieldCount}");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            count = count + 1;

                            Console.WriteLine($"\nReading {count} Row:");

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string dataType = reader.GetDataTypeName(i);
                                string columnName = reader.GetName(i);
                                object value = reader.GetValue(i);
                                Console.WriteLine($"\tColumn Name: {columnName}, Data Type: {dataType}, Value: {value}");
                            }

                            // Demonstrating GetOrdinal and IsDBNull
                            int emailColumnIndex = reader.GetOrdinal("Email");
                            if (!reader.IsDBNull(emailColumnIndex))
                            {
                                string email = reader.GetString(emailColumnIndex);
                                Console.WriteLine($"Email: {email}");
                            }
                            else
                            {
                                Console.WriteLine("Email column is DBNull.");
                            }

                            // Demonstrating GetValues
                            object[] values = new object[reader.FieldCount];
                            int numberOfValues = reader.GetValues(values);
                            Console.WriteLine($"{numberOfValues} values have been read.");

                            // Accessing data through Item property
                            string firstName = reader["FirstName"].ToString();
                            Console.WriteLine($"First Name: {firstName}");

                            // IsClosed, and RecordsAffected demonstration will be after the reader is closed to avoid disrupting the read loop.
                        }
                    }

                    // Demonstrating Close method explicitly, though it's not necessary with using statement
                    reader.Close();
                    Console.WriteLine($"\nIsClosed: {reader.IsClosed}");

                    // RecordsAffected is more relevant for update/delete operations
                    Console.WriteLine($"RecordsAffected: {command.ExecuteNonQuery()}"); // Will return -1 for SELECT operations
                    Console.ReadLine();
                }
            }
        }
    }
}