using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS
{
    public partial class TransactionForm : Form
    {
        private readonly TransactionController transactionController;

        public TransactionForm()
        {
            InitializeComponent();
            transactionController = new TransactionController();
            ConfigureListView();
            LoadTransactions();
            ConfigurePaymentMethodComboBox();
            ConfigurePaymentStatusComboBox();
            ConfigureDateTimePicker();
        }

        private void ConfigureListView()
        {
            listViewTransactions.View = View.Details;
            listViewTransactions.FullRowSelect = true;
            listViewTransactions.GridLines = true;
            listViewTransactions.Columns.Add("Transaction ID", 100);
            listViewTransactions.Columns.Add("Transaction Date", 120);
            listViewTransactions.Columns.Add("Value", 100);
            listViewTransactions.Columns.Add("Payment Method", 120);
            listViewTransactions.Columns.Add("Payment Status", 120);
        }

        private void ConfigureDateTimePicker()
        {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

        private void ConfigurePaymentMethodComboBox()
        {
            comboPaymentMethod.Items.AddRange(new string[] {
                "Cash",
                "Credit Card",
                "Debit Card",
                "Bank Transfer"
            });
            comboPaymentMethod.SelectedIndex = 0;
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

        private void LoadTransactions()
        {
            listViewTransactions.Items.Clear();
            var transactions = transactionController.GetAllTransactions();

            foreach (var transaction in transactions)
            {
                var listItem = new ListViewItem(transaction.Id.ToString());
                listItem.SubItems.Add(transaction.TransactionDate);
                listItem.SubItems.Add(transaction.Value.ToString("C"));
                listItem.SubItems.Add(transaction.PaymentMethod);
                listItem.SubItems.Add(transaction.PaymentStatus);
                listViewTransactions.Items.Add(listItem);
            }
        }

        private void ClearFields()
        {
            txtValue.Clear();
            comboPaymentMethod.SelectedIndex = 0;
            comboPaymentStatus.SelectedIndex = 0;
            dateTimePicker.Value = DateTime.Now;
            txtDeleteTransactionId.Clear();
            txtUpdateTransactionId.Clear();
        }

        private void listViewTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTransactions.SelectedItems.Count > 0)
            {
                txtUpdateTransactionId.Text = listViewTransactions.SelectedItems[0].Text;
                txtDeleteTransactionId.Text = listViewTransactions.SelectedItems[0].Text;
            }
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

            var newTransaction = new Transaction
            {
                TransactionDate = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                Value = value,
                PaymentMethod = comboPaymentMethod.SelectedItem.ToString(),
                PaymentStatus = comboPaymentStatus.SelectedItem.ToString()
            };

            transactionController.AddTransaction(newTransaction);
            MessageBox.Show("Transaction added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            LoadTransactions();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            int transactionId;

            if (!string.IsNullOrEmpty(txtDeleteTransactionId.Text) && int.TryParse(txtDeleteTransactionId.Text, out transactionId))
            {
            }
            else if (listViewTransactions.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewTransactions.SelectedItems[0].Text, out transactionId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select a transaction from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmation = MessageBox.Show("Are you sure you want to delete this transaction?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    transactionController.DeleteTransaction(transactionId);
                    MessageBox.Show("Transaction deleted successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTransactions();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting transaction: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            int transactionId;
            if (!string.IsNullOrEmpty(txtUpdateTransactionId.Text) &&
                int.TryParse(txtUpdateTransactionId.Text, out transactionId))
            {
                UpdateTransactionForm updateForm = new UpdateTransactionForm(transactionId);
                updateForm.ShowDialog();
                LoadTransactions();
            }
            else if (listViewTransactions.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewTransactions.SelectedItems[0].Text, out transactionId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdateTransactionForm updateForm = new UpdateTransactionForm(transactionId);
                updateForm.ShowDialog();
                LoadTransactions();
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select a transaction from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}