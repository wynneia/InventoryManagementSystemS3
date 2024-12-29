using System;
using System.Linq;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class UpdateProductForm : Form
    {
        private int _productId;
        private ProductController _productController;
        private CategoryController _categoryController;

        public UpdateProductForm(int productId, ProductController productController, CategoryController categoryController)
        {
            InitializeComponent();
            _productId = productId;
            _productController = productController;
            _categoryController = categoryController;

            LoadCategories();
            LoadProductDetails();
        }

        private void LoadCategories()
        {
            var categories = _categoryController.GetAllCategories();

            comboUpdateCategory.Items.Clear();
            foreach (var category in categories)
            {
                comboUpdateCategory.Items.Add(new { Text = category.Name, Value = category.Id });
            }

            comboUpdateCategory.DisplayMember = "Text";
            comboUpdateCategory.ValueMember = "Value";
        }

        private void LoadProductDetails()
        {
            var product = _productController.GetAllProducts().Find(p => p.Id == _productId);
            if (product != null)
            {
                txtUpdateName.Text = product.Name;
                txtUpdateQuantity.Text = product.Quantity.ToString();
                txtUpdateDescription.Text = product.Description;
                txtUpdateUnitPrice.Text = product.UnitPrice.ToString();
                comboUpdateCategory.SelectedItem = comboUpdateCategory.Items.Cast<dynamic>()
                    .FirstOrDefault(item => item.Value == product.CategoryId);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtUpdateName.Text;
            int? quantity = string.IsNullOrEmpty(txtUpdateQuantity.Text) ? (int?)null : int.Parse(txtUpdateQuantity.Text);
            string description = txtUpdateDescription.Text;
            double? unitPrice = string.IsNullOrEmpty(txtUpdateUnitPrice.Text) ? (double?)null : double.Parse(txtUpdateUnitPrice.Text);

            var selectedCategory = comboUpdateCategory.SelectedItem as dynamic;
            int? categoryId = selectedCategory != null ? selectedCategory.Value : (int?)null;

            _productController.UpdateProductFields(_productId, name, quantity, description, unitPrice, categoryId);

            MessageBox.Show("Product Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtUpdateName.Clear();
            txtUpdateQuantity.Clear();
            txtUpdateDescription.Clear();
            txtUpdateUnitPrice.Clear();
            comboUpdateCategory.SelectedIndex = -1;

            this.Close();
        }
    }
}
