using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class OrderForm : Form
    {
        private readonly OrderController orderController;

        public OrderForm()
        {
            InitializeComponent();
            orderController = new OrderController();
            ConfigureListView();
            LoadOrders();
            ConfigureStatusComboBox();
        }

        private void ConfigureListView()
        {
            listViewOrders.View = View.Details;
            listViewOrders.FullRowSelect = true;
            listViewOrders.GridLines = true;
            listViewOrders.Columns.Add("Order ID", 70);
            listViewOrders.Columns.Add("Order Date", 100);
            listViewOrders.Columns.Add("Order Detail", 150);
            listViewOrders.Columns.Add("Price", 70);
            listViewOrders.Columns.Add("Quantity", 70);
            listViewOrders.Columns.Add("Status", 80);
            listViewOrders.Columns.Add("Total Amount", 100);
        }

        private void ConfigureStatusComboBox()
        {
            cmbStatus.Items.AddRange(new string[] {
                "Pending", "Shipped", "Delivered", "Cancelled"
            });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOrders()
        {
            listViewOrders.Items.Clear();
            var orders = orderController.GetAllOrders();

            foreach (var order in orders)
            {
                var listItem = new ListViewItem(order.Id.ToString());
                listItem.SubItems.Add(order.OrderDate);
                listItem.SubItems.Add(order.OrderDetail);
                listItem.SubItems.Add($"${order.Price:F2}");
                listItem.SubItems.Add(order.Quantity.ToString());
                listItem.SubItems.Add(order.Status);
                listItem.SubItems.Add($"${order.TotalAmount:F2}");
                listViewOrders.Items.Add(listItem);
            }
        }


        private void ClearFields()
        {
            txtOrderDetail.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbStatus.SelectedIndex = 0;
            txtDeleteOrderId.Clear();
            txtUpdateOrderId.Clear();
        }

        private void listViewOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedItems.Count > 0)
            {
                txtUpdateOrderId.Text = listViewOrders.SelectedItems[0].Text;
                txtDeleteOrderId.Text = listViewOrders.SelectedItems[0].Text;
            }
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
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd"),
                OrderDetail = txtOrderDetail.Text,
                Price = price,
                Quantity = quantity,
                Status = cmbStatus.SelectedItem.ToString(),
                TotalAmount = price * quantity
            };

            orderController.AddOrder(order);
            LoadOrders();
            ClearFields();

            MessageBox.Show("Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int orderId;

            if (!string.IsNullOrEmpty(txtDeleteOrderId.Text) && int.TryParse(txtDeleteOrderId.Text, out orderId))
            {
            }
            else if (listViewOrders.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewOrders.SelectedItems[0].Text, out orderId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select an order from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmation = MessageBox.Show("Are you sure you want to delete this order?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    orderController.DeleteOrder(orderId);
                    MessageBox.Show("Order deleted successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting order: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int orderId;
            if (!string.IsNullOrEmpty(txtUpdateOrderId.Text) &&
                int.TryParse(txtUpdateOrderId.Text, out orderId))
            {
                UpdateOrderForm updateForm = new UpdateOrderForm(orderId);
                updateForm.ShowDialog();
                LoadOrders();
            }
            else if (listViewOrders.SelectedItems.Count > 0)
            {
                if (!int.TryParse(listViewOrders.SelectedItems[0].Text, out orderId))
                {
                    MessageBox.Show("Selected item contains invalid ID format.",
                        "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdateOrderForm updateForm = new UpdateOrderForm(orderId);
                updateForm.ShowDialog();
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Please enter a valid ID in the textbox or select an order from the list.",
                    "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}