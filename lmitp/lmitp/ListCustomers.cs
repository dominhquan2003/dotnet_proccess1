using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lmitp
{
    public partial class ListCustomers : DevExpress.XtraEditors.XtraForm
    {
        public ListCustomers()
        {
            InitializeComponent();
        }

        private void ListCusandOrder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.companyDataSet.Customers);

        }

        private void customersToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customersTableAdapter.Customers(this.companyDataSet.Customers);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}