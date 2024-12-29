using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS
{
    public partial class ReportForm : Form
    {
        private ReportController reportController;

        public ReportForm()
        {
            InitializeComponent();
            reportController = new ReportController();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            comboBoxReportType.Items.Add("Daily");
            comboBoxReportType.Items.Add("Monthly");
            comboBoxReportType.Items.Add("Custom Range");
            comboBoxReportType.SelectedIndex = 0; 

            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today;

            listViewReports.View = View.Details;
            listViewReports.Columns.Clear();
            listViewReports.Columns.Add("Type", 100);
            listViewReports.Columns.Add("ID", 50);
            listViewReports.Columns.Add("Date", 100);
            listViewReports.Columns.Add("Amount", 100);
            listViewReports.Columns.Add("Status", 100);
            listViewReports.Columns.Add("Payment Method", 100);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBoxReportType.SelectedItem.ToString();
            DateTime startDate;
            DateTime endDate;

            switch (selectedOption)
            {
                case "Daily":
                    startDate = DateTime.Today;
                    endDate = DateTime.Today;
                    break;

                case "Monthly":
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    break;

                case "Custom Range":
                    startDate = dateTimePickerStart.Value;
                    endDate = dateTimePickerEnd.Value;
                    break;

                default:
                    MessageBox.Show("Please select a valid report type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            List<Report> reports = reportController.GetReports(startDate, endDate);

            if (reports == null || reports.Count == 0)
            {
                MessageBox.Show("No reports found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DisplayReports(reports);
            }
        }

        private void DisplayReports(List<Report> reports)
        {
            listViewReports.Items.Clear();

            decimal totalAmount = 0;

            foreach (var report in reports)
            {
                var item = new ListViewItem(report.Type);
                item.SubItems.Add(report.ID.ToString());
                item.SubItems.Add(report.Date.ToShortDateString());
                item.SubItems.Add(report.Amount.ToString("C", CultureInfo.CurrentCulture));
                item.SubItems.Add(report.Status ?? "-");
                item.SubItems.Add(report.PaymentMethod ?? "-");
                item.SubItems.Add(report.PaymentStatus ?? "-");
                listViewReports.Items.Add(item);

                totalAmount += report.Amount;
            }

            lblTotalAmount.Text = $"Total: {totalAmount.ToString("C", CultureInfo.CurrentCulture)}";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            listViewReports.Items.Clear();
            lblTotalAmount.Text = "Total: $0.00";
        }
    }
}
