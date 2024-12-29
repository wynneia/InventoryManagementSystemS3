using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class SupplierForm : Form
    {
        private SupplierController _controller;

        public SupplierForm()
        {
            InitializeComponent();
            _controller = new SupplierController();
            InitializeListView();
            LoadSuppliers();
        }

        private void InitializeListView()
        {
            listViewSuppliers.View = View.Details;
            listViewSuppliers.FullRowSelect = true;
            listViewSuppliers.GridLines = true;

            listViewSuppliers.Columns.Add("Supplier ID", 90, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Supplier Name", 150, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Email", 150, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Address", 200, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Contact", 100, HorizontalAlignment.Left);
        }

        private void LoadSuppliers()
        {
            listViewSuppliers.Items.Clear();
            var suppliers = _controller.GetAllSuppliers();

            foreach (var supplier in suppliers)
            {
                var item = new ListViewItem(supplier.Id.ToString());
                item.SubItems.Add(supplier.Name);
                item.SubItems.Add(supplier.Email);
                item.SubItems.Add(supplier.Address);
                item.SubItems.Add(supplier.Contact);
                listViewSuppliers.Items.Add(item);
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtContact.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
               string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var supplier = new Supplier
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Contact = txtContact.Text
            };

            _controller.AddSupplier(supplier);
            MessageBox.Show("Supplier Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadSuppliers();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            int supplierId = 0;

            if (!string.IsNullOrEmpty(txtDeleteId.Text) && int.TryParse(txtDeleteId.Text, out supplierId))
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete this supplier?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    _controller.DeleteSupplier(supplierId);
                    MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers();
                }
            }
            else if (listViewSuppliers.SelectedItems.Count > 0)
            {
                supplierId = int.Parse(listViewSuppliers.SelectedItems[0].Text);
                var confirmation = MessageBox.Show("Are you sure you want to delete this supplier?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    _controller.DeleteSupplier(supplierId);
                    MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID or select a supplier from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            int supplierId = 0;

            if (!string.IsNullOrEmpty(txtUpdateId.Text) && int.TryParse(txtUpdateId.Text, out supplierId))
            {
                UpdateSupplierForm updateForm = new UpdateSupplierForm(supplierId);
                updateForm.ShowDialog();
                LoadSuppliers();
            }
            else if (listViewSuppliers.SelectedItems.Count > 0)
            {
                supplierId = int.Parse(listViewSuppliers.SelectedItems[0].Text);
                UpdateSupplierForm updateForm = new UpdateSupplierForm(supplierId);
                updateForm.ShowDialog();
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please enter a valid Supplier ID or select a supplier from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
