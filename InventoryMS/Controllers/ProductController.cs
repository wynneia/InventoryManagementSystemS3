using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class ProductController
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            var connection = DatabaseHelper.GetConnection();

            string query = @"
                SELECT 
                    p.Id, 
                    p.Name, 
                    p.Quantity, 
                    p.Description, 
                    p.UnitPrice, 
                    p.CategoryId, 
                    c.Name AS CategoryName 
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId = c.Id";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        UnitPrice = reader.GetDouble(4),
                        CategoryId = reader.GetInt32(5),
                        CategoryName = reader.IsDBNull(6) ? "No Category" : reader.GetString(6)
                    });
                }
            }
            return products;
        }
        
        public void AddProduct(Product product)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Products (Name, Quantity, Description, UnitPrice, CategoryId) VALUES (@Name, @Quantity, @Description, @UnitPrice, @CategoryId)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.ExecuteNonQuery();
            }
        }

        // Delete a product by ID
        public void DeleteProduct(int productId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Products WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", productId);
                command.ExecuteNonQuery();
            }
        }

        // Update product fields with optional parameters, including CategoryId
        public void UpdateProductFields(int productId, string name = null, int? quantity = null, string description = null, double? unitPrice = null, int? categoryId = null)
        {
            var connection = DatabaseHelper.GetConnection();
            var queryBuilder = new List<string>();

            if (name != null) queryBuilder.Add("Name = @Name");
            if (quantity.HasValue) queryBuilder.Add("Quantity = @Quantity");
            if (description != null) queryBuilder.Add("Description = @Description");
            if (unitPrice.HasValue) queryBuilder.Add("UnitPrice = @UnitPrice");
            if (categoryId.HasValue) queryBuilder.Add("CategoryId = @CategoryId");

            if (!queryBuilder.Any()) throw new ArgumentException("No fields provided for update.");

            string query = $"UPDATE Products SET {string.Join(", ", queryBuilder)} WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                if (name != null) command.Parameters.AddWithValue("@Name", name);
                if (quantity.HasValue) command.Parameters.AddWithValue("@Quantity", quantity.Value);
                if (description != null) command.Parameters.AddWithValue("@Description", description);
                if (unitPrice.HasValue) command.Parameters.AddWithValue("@UnitPrice", unitPrice.Value);
                if (categoryId.HasValue) command.Parameters.AddWithValue("@CategoryId", categoryId.Value);
                command.Parameters.AddWithValue("@Id", productId);
                command.ExecuteNonQuery();
            }
        }
    }
}
