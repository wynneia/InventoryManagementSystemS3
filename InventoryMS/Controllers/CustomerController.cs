using System;
using System.Collections.Generic;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class CustomerController
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Customers";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Address = reader.GetString(3),
                        Contact = reader.GetString(4)
                    });
                }
            }
            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Customers (Name, Email, Address, Contact) " +
                           "VALUES (@Name, @Email, @Address, @Contact)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Contact", customer.Contact);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.ExecuteNonQuery();
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customerId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Address = reader.GetString(3),
                            Contact = reader.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        public void UpdateCustomer(Customer customer)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "UPDATE Customers SET Name = @Name, Email = @Email, Address = @Address, Contact = @Contact " +
                           "WHERE CustomerID = @CustomerID";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Contact", customer.Contact);
                command.ExecuteNonQuery();
            }
        }
    }
}
