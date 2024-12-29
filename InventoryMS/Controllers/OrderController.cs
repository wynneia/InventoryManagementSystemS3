using System;
using System.Collections.Generic;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class OrderController
    {
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Orders";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32(0),
                        OrderDate = reader.GetString(1),
                        OrderDetail = reader.GetString(2),
                        Price = reader.GetDouble(3),
                        Quantity = reader.GetInt32(4),
                        Status = reader.GetString(5),
                        TotalAmount = reader.GetDouble(6)
                    });
                }
            }
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = null;
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Orders WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", orderId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            Id = reader.GetInt32(0),
                            OrderDate = reader.GetString(1),
                            OrderDetail = reader.GetString(2),
                            Price = reader.GetDouble(3),
                            Quantity = reader.GetInt32(4),
                            Status = reader.GetString(5),
                            TotalAmount = reader.GetDouble(6)
                        };
                    }
                }
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Orders (OrderDate, OrderDetail, Price, Quantity, Status, TotalAmount) " +
                           "VALUES (@OrderDate, @OrderDetail, @Price, @Quantity, @Status, @TotalAmount)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@OrderDetail", order.OrderDetail);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "UPDATE Orders SET OrderDate = @OrderDate, OrderDetail = @OrderDetail, Price = @Price, " +
                           "Quantity = @Quantity, Status = @Status, TotalAmount = @TotalAmount WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@OrderDetail", order.OrderDetail);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@Id", order.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int orderId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Orders WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", orderId);
                command.ExecuteNonQuery();
            }
        }
    }
}
