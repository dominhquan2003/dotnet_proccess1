using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lmitp
{
    public partial class productform : Form
    {
         /*Data Source = MYASUS\SQLEXPRESS;Initial Catalog = company; Integrated Security = True*/
        public SqlConnection conn;
        public productform()
        {
            InitializeComponent();

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = checkedListcate.SelectedIndex;        
            int count = checkedListcate.Items.Count;
            for (int x = 0; x < count; x++)
            {           
                if (index != x)
                {                   
                    checkedListcate.SetItemCheckState(x, CheckState.Unchecked);
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            txtname.Text = string.Empty;
            txtprice.Text = string.Empty;
            txtdes.Text = string.Empty;
            txtquantity.Text = string.Empty;
            checkedListcate.ClearSelected();
        }

        private void add_Click(object sender, EventArgs e)
        {
            string selectedCategory = checkedListcate.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedCategory))
            {
                MessageBox.Show("Please select a category");
                return;
            }

            string name = txtname.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name");
                return;
            }

            string description = txtdes.Text.Trim();
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter a description");
                return;
            }

            string price = txtprice.Text.Trim();
            if (string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Please enter a price");
                return;
            }

            if (!decimal.TryParse(price, out decimal parsePrice))
            {
                MessageBox.Show("Price must be a number");
                return;
            }

            string quantity = txtquantity.Text.Trim();
            if (string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please enter a quantity");
                return;
            }

            if (!int.TryParse(quantity, out int parseQuantity))
            {
                MessageBox.Show("Quantity must be a whole number");
                return;
            }

            int categoryId = GetIdCategory(selectedCategory);

            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful");

                    string insertQuery = "INSERT INTO Products (Name, Price, Description, StockQuantity, CategoryId) VALUES (@Name, @Price, @Description, @Quantity, @CategoryId)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Price", parsePrice);
                        insertCommand.Parameters.AddWithValue("@Description", description);
                        insertCommand.Parameters.AddWithValue("@Quantity", parseQuantity);
                        insertCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                        int rowsInserted = insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Inserted data successfully");
                        clear_Click(sender,e); 

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
        }

        private int GetIdCategory(string categoryName)
        {
            int categoryId = -1; // Default value to indicate no matching category found

            using (SqlConnection conn = new SqlConnection("Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True"))
            {
                conn.Open();

                string query = "SELECT Id FROM Categories WHERE Name = @CategoryName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    // Execute the query to retrieve the category ID
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        categoryId = (int)result; // Convert the result to an integer
                    }
                }
            }

            return categoryId;
        }

    }
}
