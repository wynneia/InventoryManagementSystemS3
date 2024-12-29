using System.Collections.Generic;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class TransactionController
    {
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Transactions";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        Id = reader.GetInt32(0),
                        TransactionDate = reader.GetString(1),
                        Value = reader.GetDouble(2),
                        PaymentMethod = reader.GetString(3),
                        PaymentStatus = reader.GetString(4)
                    });
                }
            }
            return transactions;
        }

        public Transaction GetTransactionById(int id)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Transactions WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Transaction
                        {
                            Id = reader.GetInt32(0),
                            TransactionDate = reader.GetString(1),
                            Value = reader.GetDouble(2),
                            PaymentMethod = reader.GetString(3),
                            PaymentStatus = reader.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        public void AddTransaction(Transaction transaction)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Transactions (TransactionDate, Value, PaymentMethod, PaymentStatus) " +
                         "VALUES (@TransactionDate, @Value, @PaymentMethod, @PaymentStatus)";

            using (var command = new SQLiteCommand(query, connection))
            {

                command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                command.Parameters.AddWithValue("@Value", transaction.Value);
                command.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod);
                command.Parameters.AddWithValue("@PaymentStatus", transaction.PaymentStatus);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "UPDATE Transactions SET TransactionDate = @TransactionDate, " +
                         "Value = @Value, PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus " +
                         "WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", transaction.Id);
                command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                command.Parameters.AddWithValue("@Value", transaction.Value);
                command.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod);
                command.Parameters.AddWithValue("@PaymentStatus", transaction.PaymentStatus);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTransaction(int transactionId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Transactions WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", transactionId);
                command.ExecuteNonQuery();
            }
        }
    }
}
