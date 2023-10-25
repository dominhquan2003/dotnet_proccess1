using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using lmitp;

namespace lmitp
{
    public partial class LoginForm : Form
    {
        public SqlConnection connection;

        public LoginForm()
        {
            InitializeComponent();

        }


        private void label6_Click(object sender, EventArgs e)
        {
            // new frmRegister().Show();
            // this.Hide();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void checkboxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';

            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection("Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Access to database successfully");
                    string selectQuery = "SELECT Email, PhoneNumber FROM Users WHERE Email = @Email AND PhoneNumber = @PhoneNumber";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", txtUsername.Text);
                        command.Parameters.AddWithValue("@PhoneNumber", txtPassword.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                Form1 mainForm = new Form1();
                                mainForm.Show(); // Show the main form
                            }
                            else
                            {

                                MessageBox.Show("Incorrect login information. Please check your email and password.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to the database: " + ex.Message);
                }
            }


        }
    }
}