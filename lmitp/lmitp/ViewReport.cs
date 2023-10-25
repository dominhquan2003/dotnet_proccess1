using Microsoft.Reporting.WinForms;
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
    public partial class ViewReport : Form
    {
        public ViewReport()
        {
            InitializeComponent();
        }

        private void ViewReport_Load(object sender, EventArgs e)
        {
            DataSet1 report_ds = new DataSet1();

            // Define the connection string to your SQL Server database
            string connection_string = "Data Source=MYASUS\\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";


            string query = "SELECT Id, OrderDate, TotalAmount, CustomerId FROM  Orders";

            SqlConnection conn = new SqlConnection(connection_string);
            SqlDataAdapter adptr = new SqlDataAdapter(query, conn);


            adptr.Fill(report_ds, report_ds.Tables[0].TableName);


            ReportDataSource rds = new ReportDataSource("DataSet1", report_ds.Tables[0]);

            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();        

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
