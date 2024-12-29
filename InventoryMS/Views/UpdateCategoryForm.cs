using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class UpdateCategoryForm : Form
    {
        private int _categoryId;
        private CategoryController _categoryController;

        public UpdateCategoryForm(int categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;
            _categoryController = new CategoryController();
            LoadCategoryDetails();
        }

        private void LoadCategoryDetails()
        {
            var category = _categoryController.GetCategoryById(_categoryId);
            txtCategoryName.Text = category.Name;
            txtCategoryDescription.Text = category.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var category = new Category
            {
                Id = _categoryId,
                Name = txtCategoryName.Text,
                Description = txtCategoryDescription.Text
            };

            _categoryController.UpdateCategory(category);
            MessageBox.Show("Category updated successfully!");
            this.Close();
        }
    }
}
