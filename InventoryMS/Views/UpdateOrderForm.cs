using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class UpdateOrderForm : Form
    {
        private readonly OrderController orderController;
        private readonly int orderId;

        public UpdateOrderForm(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            orderController = new OrderController();
            LoadOrderDetails();
            ConfigureStatusComboBox();
        }

        private void LoadOrderDetails()
        {
            var order = orderController.GetOrderById(orderId);

            if (order != null)
            {
                txtOrderDetail.Text = order.OrderDetail;
                txtPrice.Text = order.Price.ToString("F2");
                txtQuantity.Text = order.Quantity.ToString();
                cmbStatus.SelectedItem = order.Status;
                dateTimePicker.Value = DateTime.Parse(order.OrderDate);
            }
            else
            {
                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ConfigureStatusComboBox()
        {
            cmbStatus.Items.AddRange(new string[] {
                "Pending", "Shipped", "Delivered", "Cancelled"
            });
            cmbStatus.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderDetail.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
               string.IsNullOrWhiteSpace(txtQuantity.Text) || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtPrice.Text, out double price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var order = new Order
            {
                Id = orderId,
                OrderDetail = txtOrderDetail.Text,
                Price = price,
                Quantity = quantity,
                Status = cmbStatus.SelectedItem.ToString(),
                OrderDate = dateTimePicker.Value.ToString("yyyy-MM-dd"),
                TotalAmount = price * quantity
            };

            try
            {
                orderController.UpdateOrder(order);
                MessageBox.Show("Order updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}