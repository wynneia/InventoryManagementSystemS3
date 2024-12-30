using System;
using System.IO;
using System.Data.SQLite;

namespace InventoryMS.Models
{
    public class DatabaseHelper
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string dbPath = Path.Combine(documentsPath, "InventoryMS", "InventoryMS", "Inventory.db");
                _connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
                _connection.Open();
            }
            return _connection;
        }
    }
}
