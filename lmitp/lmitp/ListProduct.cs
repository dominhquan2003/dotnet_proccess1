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
    public partial class ListProduct : DevExpress.XtraEditors.XtraForm
    {
        public ListProduct()
        {
            InitializeComponent();
        }

        private void ListProduct_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.companyDataSet.Products);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter.FillBy(this.companyDataSet.Products);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter.FillBy1(this.companyDataSet.Products);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void productsToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter.Products(this.companyDataSet.Products);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}