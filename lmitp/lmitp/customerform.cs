using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lmitp
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            txtname.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtphone.Text = string.Empty;
        }

        private void add_Click(object sender, EventArgs e)
        {
            // Get the values from the text fields
            string name = txtname.Text;
            string address = txtaddress.Text;
            string email = txtemail.Text;
            string phone = txtphone.Text;

            // Validate the phone number
            if (!Regex.IsMatch(phone, "^[0-9]{10}$"))
            {
                MessageBox.Show("Phone number must be a 10-digit number.");
                return; // Exit the function if the phone number is invalid
            }

            // Connection string
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the phone number already exists in the Customers table
                string checkQuery = "SELECT COUNT(*) FROM Customers WHERE Phone = @Phone";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Phone", phone);
                    int customerCount = (int)checkCmd.ExecuteScalar();

                    if (customerCount > 0)
                    {
                        MessageBox.Show("Phone number already exists in the database.");
                        return; // Exit the function if the phone number already exists
                    }
                }

                // SQL INSERT statement
                string insertQuery = "INSERT INTO Customers (Name, Address, Email, Phone) VALUES (@Name, @Address, @Email, @Phone)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    // Add parameters to the SQL query to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    // Execute the SQL query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer added successfully.");
                        clear_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add the customer.");
                    }
                }
            }

        }
    }
}
