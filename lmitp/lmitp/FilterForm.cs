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
using System.Xml.Linq;

namespace lmitp
{
    public partial class FilterForm : Form
    {
        public FilterForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListcate_SelectedIndexChanged(object sender, EventArgs e)
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

            if (checkedListcate.CheckedItems.Contains("Best product"))
            {
                // Nếu được chọn, chuyển txtfilter thành read-only
                txtfilter.ReadOnly = true;
                txtfilter.Text = ""; 
            }
            else
            {
                // Nếu không được chọn, cho phép chỉnh sửa txtfilter
                txtfilter.ReadOnly = false;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedTable = "";
            string connectionString = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful");

                    if (checkedListcate.CheckedItems.Contains("Products"))
                    {
                        selectedTable = "Products";
                        Console.WriteLine("Products checkbox status: " + checkedListcate.CheckedItems.Contains("Products"));
                        string query = "SELECT C.Name AS CustomerName, O.OrderDate, P.Name AS ProductName, OD.Quantity AS QuantityPurchased FROM Customers C JOIN Orders O ON C.Id = O.CustomerId JOIN OrderDetails OD ON O.Id = OD.OrderId JOIN Products P ON OD.ProductsId = P.Id WHERE P.Name LIKE @ProductName ORDER BY C.Name, O.OrderDate;";
                        using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                        {
                            sqlCommand.Parameters.AddWithValue("@ProductName", "%" + txtfilter.Text.Trim() + "%");

                            // Create a DataTable to hold the query results
                            DataTable dt = new DataTable();

                            // Use a SqlDataAdapter to fill the DataTable
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);

                            // Set the DataGridView's data source to the DataTable
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Đã tìm thấy sản phẩm");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sản phẩm");
                            }
                        }
                    }
                    else if (checkedListcate.CheckedItems.Contains("Customers"))
                    {
                        selectedTable = "Customers";
                        Console.WriteLine("Products checkbox status: " + checkedListcate.CheckedItems.Contains("Customers"));

                        string query = "SELECT C.Name AS CustomerName, P.Name AS ProductName, OD.Quantity AS QuantityPurchased, O.OrderDate FROM Customers C JOIN Orders O ON C.Id = O.CustomerId JOIN OrderDetails OD ON O.Id = OD.OrderId JOIN Products P ON OD.ProductsId = P.Id WHERE C.Name LIKE @CustomerName ORDER BY C.Name, O.OrderDate";
                        using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                        {
                            sqlCommand.Parameters.AddWithValue("@CustomerName", "%" + txtfilter.Text.Trim() + "%");

                            // Create a DataTable to hold the query results
                            DataTable dt = new DataTable();

                            // Use a SqlDataAdapter to fill the DataTable
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);

                            // Set the DataGridView's data source to the DataTable
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Đã tìm thấy khách hàng");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng");
                            }
                        }
                    }
                    else if (checkedListcate.CheckedItems.Contains("Best product"))
                    {
                        selectedTable = "Best product";
                        Console.WriteLine("Products checkbox status: " + checkedListcate.CheckedItems.Contains("Best product"));
                        string query = "SELECT * FROM Products WHERE Price = (SELECT MAX(Price) FROM Products)";

                        using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                        {
                            // Create a DataTable to hold the query results
                            DataTable dt = new DataTable();

                            // Use a SqlDataAdapter to fill the DataTable
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);

                            // Set the DataGridView's data source to the DataTable
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Đã tìm thấy sản phẩm");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sản phẩm");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn loại dữ liệu để tìm kiếm");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
        }

    }
}
