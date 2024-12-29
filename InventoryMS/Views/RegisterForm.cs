using System;
using System.Windows.Forms;
using InventoryMS.Controllers;

namespace InventoryMS.Views
{
    public partial class RegisterForm : Form
    {
        private UserController _userController;

        public RegisterForm()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Register the user
            _userController.AddUser(username, password);

            MessageBox.Show("Registration successful!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
