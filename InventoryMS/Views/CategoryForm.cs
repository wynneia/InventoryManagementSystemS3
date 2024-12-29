using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class CategoryForm : Form
    {
        private CategoryController _categoryController;

        public CategoryForm()
        {
            InitializeComponent();
            _categoryController = new CategoryController();
            InitializeListView();
            LoadCategories();
        }

        private void InitializeListView()
        {
            listViewCategories.Columns.Add("Category ID", 100, HorizontalAlignment.Left);
            listViewCategories.Columns.Add("Name", 200, HorizontalAlignment.Left);
            listViewCategories.Columns.Add("Description", 250, HorizontalAlignment.Left);

            listViewCategories.View = View.Details;
            listViewCategories.FullRowSelect = true;
            listViewCategories.GridLines = true;
        }

        private void LoadCategories()
        {
            listViewCategories.Items.Clear();
            var categories = _categoryController.GetAllCategories();

            foreach (var category in categories)
            {
                ListViewItem item = new ListViewItem(category.Id.ToString());
                item.SubItems.Add(category.Name);
                item.SubItems.Add(category.Description);

                listViewCategories.Items.Add(item);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var category = new Category
            {
                Name = txtCategoryName.Text,
                Description = txtCategoryDescription.Text
            };

            _categoryController.AddCategory(category);

            MessageBox.Show("Category added successfully!");
            LoadCategories();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int categoryId;

            if (int.TryParse(txtDeleteCategoryId.Text, out categoryId) || listViewCategories.SelectedItems.Count > 0)
            {
                if (listViewCategories.SelectedItems.Count > 0)
                {
                    categoryId = int.Parse(listViewCategories.SelectedItems[0].Text);
                }

                var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _categoryController.DeleteCategory(categoryId);
                    MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategories();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Category ID or select a category from the list.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void update_Click(object sender, EventArgs e)
        {
            int categoryId;

            if (listViewCategories.SelectedItems.Count > 0)
            {
                var selectedItem = listViewCategories.SelectedItems[0];
                categoryId = int.Parse(selectedItem.Text);

                UpdateCategoryForm updateForm = new UpdateCategoryForm(categoryId);
                updateForm.ShowDialog();

                LoadCategories();
            }
            else if (int.TryParse(txtDeleteCategoryId.Text, out categoryId))
            {
                UpdateCategoryForm updateForm = new UpdateCategoryForm(categoryId);
                updateForm.ShowDialog();

                LoadCategories();
            }
            else
            {
                MessageBox.Show("Please select a category to update or enter a valid Category ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
