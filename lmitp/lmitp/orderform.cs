using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.About;
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
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace lmitp
{
    public partial class OrderForm : Form
    {
        public SqlConnection conn;
        public OrderForm()
        {
            InitializeComponent();
            txtcusid.TextChanged += txtcusid_TextChanged;
        }
        private void txtcusid_TextChanged(object sender, EventArgs e)
        {
            // Check if the txtcusid TextBox is not empty
            if (!string.IsNullOrEmpty(txtcusid.Text))
            {
                // Assign the orderdate.Text with the current date and time
                orderdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                LatestOrderID();



            }
            else
            {
                // Clear the orderdate.Text if txtcusid is empty
                orderdate.Text = string.Empty;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            txtcusid.Text = string.Empty;
            orderdate.Text = string.Empty;
            txttotalamount.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }
        private decimal UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["totalprice"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["totalprice"].Value);
                }
            }

            txttotalamount.Text = total.ToString();
            return total; 
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["quantity"].Index)
            {
                
                int quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["product"].Value);               
                decimal price = GetPriceFromProductId(productId);

                
                decimal totalprice = quantity * price;
                dataGridView1.Rows[e.RowIndex].Cells["price"].Value = price;
                dataGridView1.Rows[e.RowIndex].Cells["totalprice"].Value = totalprice;
                UpdateTotal();
            }
        }



        private decimal GetPriceFromProductId(int productId)
        {
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";
            decimal price = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful");

                    string selectQuery = "SELECT Price FROM Products WHERE ID = @ProductId";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, conn))
                    {
                        selectCommand.Parameters.AddWithValue("@ProductId", productId);                       
                        object result = selectCommand.ExecuteScalar();

                        if (result != null)
                        {
                            price = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }

            return price;
        }

        private int LatestOrderID()
        {
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True"; 
            int latestOrderID = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MAX(Id) FROM Orders"; // 

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        latestOrderID = Convert.ToInt32(result) + 1 ;
                        orderid.Text = latestOrderID.ToString() ;
                    }
                }
            }
            return latestOrderID; 

            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            decimal totalAmount = UpdateTotal();
            string cusId = txtcusid.Text;

            // Validate customer ID
            if (string.IsNullOrEmpty(cusId))
            {
                MessageBox.Show("Please enter a customer ID.");
                return;
            }

            if (!int.TryParse(cusId, out int parseCusID))
            {
                MessageBox.Show("Customer ID must be a number.");
                return;
            }

            string dateTime = orderdate.Text;
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Start a SQL transaction for inserting orders and order details

                try
                {
                    Console.WriteLine("Connection successful");

                    // Insert the order
                    string insertOrderQuery = "INSERT INTO Orders (OrderDate, TotalAmount, CustomerId) VALUES (@OrderDate, @TotalAmount, @CustomerId); SELECT SCOPE_IDENTITY()";

                    using (SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, conn, transaction))
                    {
                        insertOrderCommand.Parameters.AddWithValue("@OrderDate", dateTime);
                        insertOrderCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        insertOrderCommand.Parameters.AddWithValue("@CustomerId", parseCusID);
                        
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            DataGridViewRow row = dataGridView1.Rows[i];
                            int productId = Convert.ToInt32(row.Cells["product"].Value);
                            if (!ProductExists(productId))
                            {
                                MessageBox.Show($"Product with ID {productId} does not exist.");
                                transaction.Rollback();
                                return;
                            }
                            int quantity = Convert.ToInt32(row.Cells["quantity"].Value);
                            decimal totalPrice = Convert.ToDecimal(row.Cells["totalprice"].Value);
                            int orderId = LatestOrderID() - 1; 
                            // Insert OrderDetail
                            string insertOrderDetailQuery = "INSERT INTO OrderDetails (OrderId, Quantity, TotalPrice, ProductsId) VALUES (@OrderId, @Quantity, @TotalPrice, @ProductsId)";

                            using (SqlCommand insertOrderDetailCommand = new SqlCommand(insertOrderDetailQuery, conn, transaction))
                            {
                                insertOrderDetailCommand.Parameters.AddWithValue("@OrderId", orderId);
                                insertOrderDetailCommand.Parameters.AddWithValue("@Quantity", quantity);
                                insertOrderDetailCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                                insertOrderDetailCommand.Parameters.AddWithValue("@ProductsId", productId);

                                insertOrderDetailCommand.ExecuteNonQuery(); // Insert the OrderDetail
                            }
                        }
                        insertOrderCommand.ExecuteNonQuery();

                        transaction.Commit(); // Commit the transaction
                        MessageBox.Show("Order and OrderDetails inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback the transaction if an error occurs
                    Console.WriteLine("Connection failed: " + ex.Message);
                    MessageBox.Show("An error occurred while inserting the order.");
                }
            }
        }

        private bool ProductExists(int productId)
        {
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";
            bool exists = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Define the SQL query to check if the product with the given ID exists
                string query = "SELECT 1 FROM Products WHERE Id = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    // Execute the query and check if any rows are returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    exists = reader.HasRows;
                }
            }

            return exists;
        }
        private void SetupReportViewer()
        {
           
        }

        private void btnprint_Click(object sender, EventArgs e)
        {

        }
    }

}
