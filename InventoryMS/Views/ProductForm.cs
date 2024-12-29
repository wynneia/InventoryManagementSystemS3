using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;
using System.Linq;

namespace InventoryMS.Views
{
    public partial class ProductForm : Form
    {
        private ProductController _productController;
        private CategoryController _categoryController; // Added for categories

        public ProductForm()
        {
            InitializeComponent();
            _productController = new ProductController();
            _categoryController = new CategoryController();
            InitializeListView();
            LoadProducts();
            LoadCategories(); // Load categories into the dropdown
        }

        private void InitializeListView()
        {
            listViewProducts.Columns.Add("Product ID", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Product Name", 180, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Description", 180, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Quantity", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Unit Price", 100, HorizontalAlignment.Right);
            listViewProducts.Columns.Add("Category Name", 150, HorizontalAlignment.Left);

            listViewProducts.View = View.Details;
            listViewProducts.FullRowSelect = true;
            listViewProducts.GridLines = true;
        }

        private void LoadProducts()
        {
            listViewProducts.Items.Clear();
            var products = _productController.GetAllProducts();

            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Description);
                item.SubItems.Add(product.Quantity.ToString());
                item.SubItems.Add(product.UnitPrice.ToString("C"));
                item.SubItems.Add(product.CategoryName);

                listViewProducts.Items.Add(item);
            }
        }

        private void LoadCategories()
        {
            var categories = _categoryController.GetAllCategories();

            // Populate dropdown for adding and updating products
            comboCategory.Items.Clear();

            foreach (var category in categories)
            {
                comboCategory.Items.Add(new { Text = category.Name, Value = category.Id });
            }

            comboCategory.DisplayMember = "Text";
            comboCategory.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int productId = 0;

            // Check if the product ID is entered or selected from ListView
            if (!string.IsNullOrEmpty(txtUpdateId.Text) && int.TryParse(txtUpdateId.Text, out productId))
            {
                var updateForm = new UpdateProductForm(productId, _productController, _categoryController);
                updateForm.FormClosed += (s, args) => LoadProducts(); // Refresh products after updating
                txtUpdateId.Text = "";
                updateForm.Show();
            }
            else if (listViewProducts.SelectedItems.Count > 0)
            {
                productId = int.Parse(listViewProducts.SelectedItems[0].Text);
                var updateForm = new UpdateProductForm(productId, _productController, _categoryController);
                updateForm.FormClosed += (s, args) => LoadProducts(); // Refresh products after updating
                updateForm.Show();
            }
            else
            {
                MessageBox.Show("Please enter a valid ID or select a product from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (comboCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtUnitPrice.Text, out double unitPrice))
            {
                MessageBox.Show("Please enter a valid unit price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCategory = (dynamic)comboCategory.SelectedItem;
            var product = new Product
            {
                Name = txtName.Text,
                Quantity = quantity,
                Description = txtDescription.Text,
                UnitPrice = unitPrice,
                CategoryId = selectedCategory.Value
            };

            _productController.AddProduct(product);
            MessageBox.Show("Product Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtName.Text = "";
            txtQuantity.Text = "";
            txtDescription.Text = "";
            txtUnitPrice.Text = "";
            comboCategory.SelectedIndex = -1;
            LoadProducts();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            int productId = 0;

            // Check if the product ID is entered or selected from ListView
            if (!string.IsNullOrEmpty(txtDeleteId.Text) && int.TryParse(txtDeleteId.Text, out productId))
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    _productController.DeleteProduct(productId);
                    MessageBox.Show("Product Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDeleteId.Text = "";
                    LoadProducts();
                }
            }
            else if (listViewProducts.SelectedItems.Count > 0)
            {
                productId = int.Parse(listViewProducts.SelectedItems[0].Text);
                var confirmation = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    _productController.DeleteProduct(productId);
                    MessageBox.Show("Product Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID or select a product from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Add validation for numeric input
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, decimal point, and control keys (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            // Prevent multiple decimal points
            if (e.KeyChar == 46 && txtUnitPrice.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
