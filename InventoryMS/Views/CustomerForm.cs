using System;
using System.Windows.Forms;
using System.Xml.Linq;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerController customerController;

        public CustomerForm()
        {
            InitializeComponent();
            customerController = new CustomerController();
            ConfigureListView();
            LoadCustomers();
        }

        private void ConfigureListView()
        {
            listViewCustomers.View = View.Details;
            listViewCustomers.FullRowSelect = true;
            listViewCustomers.GridLines = true;

            listViewCustomers.Columns.Add("Customer ID", 100);
            listViewCustomers.Columns.Add("Name", 150);
            listViewCustomers.Columns.Add("Email", 150);
            listViewCustomers.Columns.Add("Address", 200);
            listViewCustomers.Columns.Add("Contact", 100);
        }

        private void LoadCustomers()
        {
            listViewCustomers.Items.Clear();
            var customers = customerController.GetAllCustomers();

            foreach (var customer in customers)
            {
                var listItem = new ListViewItem(customer.CustomerID.ToString());
                listItem.SubItems.Add(customer.Name);
                listItem.SubItems.Add(customer.Email);
                listItem.SubItems.Add(customer.Address);
                listItem.SubItems.Add(customer.Contact);
                listViewCustomers.Items.Add(listItem);
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtContact.Clear();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCustomer = new Customer
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Contact = txtContact.Text
            };

            customerController.AddCustomer(newCustomer);
            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearFields();
            LoadCustomers();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            int customerId;

            if (!string.IsNullOrEmpty(txtDeleteCustomerId.Text) && int.TryParse(txtDeleteCustomerId.Text, out customerId))
            {
            }
            else if (listViewCustomers.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewCustomers.SelectedItems[0].Text, out customerId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select a customer from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmation = MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    customerController.DeleteCustomer(customerId);
                    MessageBox.Show("Customer deleted successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int customerId;

            if (!string.IsNullOrEmpty(txtUpdateCustomerId.Text) && int.TryParse(txtDeleteCustomerId.Text, out customerId))
            {

            }
            else if (listViewCustomers.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewCustomers.SelectedItems[0].Text, out customerId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select a customer from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UpdateCustomerForm updateForm = new UpdateCustomerForm(customerId);
            updateForm.ShowDialog();
            LoadCustomers();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void txtUpdateCustomerId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
