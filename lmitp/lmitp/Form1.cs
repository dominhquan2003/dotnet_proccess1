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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        

        private void btnemp_Click(object sender, EventArgs e)
        {
            loadform(new CustomerForm());
        }

        private void btnreports_Click(object sender, EventArgs e)
        {
            loadform(new ViewReport());
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new productform());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new ListProduct());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadform(new ListCustomers()); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadform(new OrderForm()); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadform(new ListOrder());
        }

        private void FILTER_Click(object sender, EventArgs e)
        {
            loadform(new FilterForm()); 
        }
    }
}
