using System;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class UserController
    {
        public bool VerifyUser(string username, string password)
        {
            bool isValid = false;

            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Users WHERE Username = @Username";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPasswordHash = reader.GetString(2);
                        isValid = PasswordHelper.VerifyPassword(password, storedPasswordHash);
                    }
                }
            }

            return isValid;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }

        public void AddUser(string username, string password)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            string hashedPassword = PasswordHelper.HashPassword(password);

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword); // Ensure password is hashed before storing

                // Open the connection if it's not already open
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.ExecuteNonQuery();
            }
        }
    }
}
