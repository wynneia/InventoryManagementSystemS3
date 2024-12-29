using InventoryMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var productForm = new ProductForm();
            productForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var categoryForm = new CategoryForm();
            categoryForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var orderForm = new OrderForm();
            orderForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var supplierForm = new SupplierForm();
            supplierForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var customerForm = new CustomerForm();
            customerForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var transactionForm = new TransactionForm();
            transactionForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportForm();
            reportForm.Show();
        }
    }
}
