using InventoryMS.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System;

public class ReportController
{
    public List<Report> GetReports(DateTime startDate, DateTime endDate)
    {
        List<Report> reports = new List<Report>();
        var connection = DatabaseHelper.GetConnection();

        // Fetch orders
        string orderQuery = @"
                SELECT Id, OrderDate, TotalAmount, Status 
                FROM Orders 
                WHERE DATE(OrderDate) BETWEEN DATE(@StartDate) AND DATE(@EndDate)";
        using (var command = new SQLiteCommand(orderQuery, connection))
        {
            command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    reports.Add(new Report
                    {
                        Type = "Order",
                        ID = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1)),
                        Amount = reader.GetDecimal(2),
                        Status = reader.GetString(3),
                        PaymentMethod = null,
                        PaymentStatus = null
                    });
                }
            }
        }

        // Fetch transactions
        string transactionQuery = @"
                SELECT Id, TransactionDate, Value, PaymentMethod, PaymentStatus 
                FROM Transactions 
                WHERE DATE(TransactionDate) BETWEEN DATE(@StartDate) AND DATE(@EndDate)";
        using (var command = new SQLiteCommand(transactionQuery, connection))
        {
            command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    reports.Add(new Report
                    {
                        Type = "Transaction",
                        ID = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1)),
                        Amount = reader.GetDecimal(2),
                        PaymentMethod = reader.GetString(3),
                        Status = reader.IsDBNull(4) ? "-" : reader.GetString(4),
                        PaymentStatus = reader.IsDBNull(4) ? "-" : reader.GetString(4)
                    });
                }
            }
        }
        return reports;
    }

    public decimal GetTotalAmount(List<Report> reports)
    {
        decimal totalAmount = 0;
        foreach (var report in reports)
        {
            totalAmount += report.Amount;
        }
        return totalAmount;
    }
}