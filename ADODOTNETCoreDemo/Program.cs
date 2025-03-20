using Microsoft.Data.SqlClient;
namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
            string connectionString = "Server=DESKTOP-RUC57UF;Database=OrderDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch customers
                    string customerQuery = "SELECT * FROM Customers;";
                    using (SqlCommand customerCommand = new SqlCommand(customerQuery, connection))
                    {
                        using (SqlDataReader customerReader = customerCommand.ExecuteReader())
                        {
                            while (customerReader.Read())
                            {
                                var customer = new Customer
                                {
                                    CustomerID = (int)customerReader["CustomerID"],
                                    Name = customerReader["Name"].ToString(),
                                    Email = customerReader["Email"].ToString(),
                                    Address = customerReader["Address"].ToString()
                                };
                                customers.Add(customer.CustomerID, customer);
                            }
                        }
                    }

                    Console.WriteLine("Customer and Orders:");

                    // Fetch orders and match with customers
                    string orderQuery = "SELECT * FROM Orders;";
                    using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection))
                    {
                        using (SqlDataReader orderReader = orderCommand.ExecuteReader())
                        {
                            while (orderReader.Read())
                            {
                                var order = new Order
                                {
                                    OrderID = (int)orderReader["OrderID"],
                                    CustomerID = (int)orderReader["CustomerID"],
                                    OrderDate = (DateTime)orderReader["OrderDate"],
                                    TotalAmount = (decimal)orderReader["TotalAmount"]
                                };

                                // Match order with customer
                                if (customers.TryGetValue(order.CustomerID, out Customer customer))
                                {
                                    Console.WriteLine($"CustomerID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}, Address: {customer.Address}");
                                    Console.WriteLine($"\tOrderID: {order.OrderID}, OrderDate: {order.OrderDate}, TotalAmount: {order.TotalAmount}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}