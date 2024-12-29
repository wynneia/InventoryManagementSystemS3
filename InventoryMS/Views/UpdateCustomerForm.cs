using System;
using System.Windows.Forms;
using System.Xml.Linq;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS
{
    public partial class UpdateCustomerForm : Form
    {
        private readonly CustomerController customerController;
        private readonly int customerId;

        public UpdateCustomerForm(int customerId)
        {
            InitializeComponent();
            customerController = new CustomerController();
            this.customerId = customerId;

            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            Customer customer = customerController.GetCustomerById(customerId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;
                txtContact.Text = customer.Contact;
            }
            else
            {
                MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
               string.IsNullOrWhiteSpace(txtEmail.Text) ||
               string.IsNullOrWhiteSpace(txtAddress.Text) ||
               string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer updatedCustomer = new Customer
            {
                CustomerID = customerId,
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Contact = txtContact.Text
            };

            customerController.UpdateCustomer(updatedCustomer);
            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
