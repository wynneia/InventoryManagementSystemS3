using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS
{
    public partial class UpdateTransactionForm : Form
    {
        private readonly TransactionController transactionController;
        private readonly int transactionId;
        private Transaction currentTransaction;

        public UpdateTransactionForm(int id)
        {
            InitializeComponent();
            transactionController = new TransactionController();
            transactionId = id;
            ConfigurePaymentMethodComboBox();
            ConfigurePaymentStatusComboBox();
            ConfigureDateTimePicker();
            LoadTransactionData();
        }

        private void ConfigurePaymentMethodComboBox()
        {
            comboPaymentMethod.Items.AddRange(new string[] {
                "Cash",
                "Credit Card",
                "Debit Card",
                "Bank Transfer"
            });
        }

        private void ConfigurePaymentStatusComboBox()
        {
            comboPaymentStatus.Items.AddRange(new string[] {
                "Pending",
                "Completed",
                "Failed",
                "Cancelled"
            });
            comboPaymentStatus.SelectedIndex = 0;
        }

        private void ConfigureDateTimePicker()
        {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

        private void LoadTransactionData()
        {
            currentTransaction = transactionController.GetTransactionById(transactionId);
            if (currentTransaction != null)
            {
                dateTimePicker.Value = DateTime.Parse(currentTransaction.TransactionDate);
                txtValue.Text = currentTransaction.Value.ToString();
                comboPaymentMethod.SelectedItem = currentTransaction.PaymentMethod;
                comboPaymentStatus.SelectedItem = currentTransaction.PaymentStatus;
            }
            else
            {
                MessageBox.Show("Transaction not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateTransactionForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                MessageBox.Show("Please enter a value.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtValue.Text, out double value))
            {
                MessageBox.Show("Please enter a valid value.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentTransaction.TransactionDate = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
            currentTransaction.Value = value;
            currentTransaction.PaymentMethod = comboPaymentMethod.SelectedItem.ToString();
            currentTransaction.PaymentStatus = comboPaymentStatus.SelectedItem.ToString();

            try
            {
                transactionController.UpdateTransaction(currentTransaction);
                MessageBox.Show("Transaction updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating transaction: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}