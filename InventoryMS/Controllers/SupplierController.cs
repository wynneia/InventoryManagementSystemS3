using System;
using System.Collections.Generic;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class SupplierController
    {
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Suppliers";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    suppliers.Add(new Supplier
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Address = reader.GetString(3),
                        Contact = reader.GetString(4)
                    });
                }
            }
            return suppliers;
        }

        public void AddSupplier(Supplier supplier)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Suppliers (Name, Email, Address, Contact) " +
                           "VALUES (@Name, @Email, @Address, @Contact)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", supplier.Name);
                command.Parameters.AddWithValue("@Email", supplier.Email);
                command.Parameters.AddWithValue("@Address", supplier.Address);
                command.Parameters.AddWithValue("@Contact", supplier.Contact);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Suppliers WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", supplierId);
                command.ExecuteNonQuery();
            }
        }

        public Supplier GetSupplierById(int supplierId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Suppliers WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", supplierId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Supplier
                        {
                            Id = reader.GetInt32(0),
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

        public void UpdateSupplier(Supplier supplier)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "UPDATE Suppliers SET Name = @Name, Email = @Email, Address = @Address, Contact = @Contact " +
                           "WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", supplier.Id);
                command.Parameters.AddWithValue("@Name", supplier.Name);
                command.Parameters.AddWithValue("@Email", supplier.Email);
                command.Parameters.AddWithValue("@Address", supplier.Address);
                command.Parameters.AddWithValue("@Contact", supplier.Contact);
                command.ExecuteNonQuery();
            }
        }
    }
}
