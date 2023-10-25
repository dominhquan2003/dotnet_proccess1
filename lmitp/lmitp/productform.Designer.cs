namespace lmitp
{
    partial class productform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.des = new System.Windows.Forms.Label();
            this.quantity = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtprice = new System.Windows.Forms.TextBox();
            this.txtdes = new System.Windows.Forms.RichTextBox();
            this.txtquantity = new System.Windows.Forms.TextBox();
            this.checkedListcate = new System.Windows.Forms.CheckedListBox();
            this.category = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 24F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(149, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Form";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.name.Location = new System.Drawing.Point(24, 62);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(55, 20);
            this.name.TabIndex = 1;
            this.name.Text = "Name ";
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.price.Location = new System.Drawing.Point(24, 98);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(44, 20);
            this.price.TabIndex = 2;
            this.price.Text = "Price";
            // 
            // des
            // 
            this.des.AutoSize = true;
            this.des.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.des.Location = new System.Drawing.Point(12, 265);
            this.des.Name = "des";
            this.des.Size = new System.Drawing.Size(89, 20);
            this.des.TabIndex = 3;
            this.des.Text = "Description";
            // 
            // quantity
            // 
            this.quantity.AutoSize = true;
            this.quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.quantity.Location = new System.Drawing.Point(0, 134);
            this.quantity.Name = "quantity";
            this.quantity.Size = new System.Drawing.Size(113, 20);
            this.quantity.TabIndex = 5;
            this.quantity.Text = "Stock Quantity";
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(135, 64);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(432, 20);
            this.txtname.TabIndex = 6;
            // 
            // txtprice
            // 
            this.txtprice.Location = new System.Drawing.Point(135, 98);
            this.txtprice.Name = "txtprice";
            this.txtprice.Size = new System.Drawing.Size(278, 20);
            this.txtprice.TabIndex = 7;
            // 
            // txtdes
            // 
            this.txtdes.Location = new System.Drawing.Point(135, 265);
            this.txtdes.Name = "txtdes";
            this.txtdes.Size = new System.Drawing.Size(432, 74);
            this.txtdes.TabIndex = 9;
            this.txtdes.Text = "";
            // 
            // txtquantity
            // 
            this.txtquantity.Location = new System.Drawing.Point(135, 134);
            this.txtquantity.Name = "txtquantity";
            this.txtquantity.Size = new System.Drawing.Size(278, 20);
            this.txtquantity.TabIndex = 10;
            // 
            // checkedListcate
            // 
            this.checkedListcate.CheckOnClick = true;
            this.checkedListcate.FormattingEnabled = true;
            this.checkedListcate.Items.AddRange(new object[] {
            "Electronics",
            "Clothing",
            "Books",
            "Kitchen Appliances",
            "Sports Equipment"});
            this.checkedListcate.Location = new System.Drawing.Point(135, 180);
            this.checkedListcate.Name = "checkedListcate";
            this.checkedListcate.Size = new System.Drawing.Size(159, 49);
            this.checkedListcate.TabIndex = 11;
            this.checkedListcate.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // category
            // 
            this.category.AutoSize = true;
            this.category.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.category.Location = new System.Drawing.Point(12, 180);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(73, 20);
            this.category.TabIndex = 12;
            this.category.Text = "Category";
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.Color.Silver;
            this.clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear.ForeColor = System.Drawing.Color.Black;
            this.clear.Location = new System.Drawing.Point(102, 370);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(168, 28);
            this.clear.TabIndex = 13;
            this.clear.Text = "CLEAR";
            this.clear.UseVisualStyleBackColor = false;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add.ForeColor = System.Drawing.Color.Black;
            this.add.Location = new System.Drawing.Point(286, 370);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(172, 28);
            this.add.TabIndex = 14;
            this.add.Text = "ADD";
            this.add.UseVisualStyleBackColor = false;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // productform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 420);
            this.ControlBox = false;
            this.Controls.Add(this.add);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.category);
            this.Controls.Add(this.checkedListcate);
            this.Controls.Add(this.txtquantity);
            this.Controls.Add(this.txtdes);
            this.Controls.Add(this.txtprice);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.quantity);
            this.Controls.Add(this.des);
            this.Controls.Add(this.price);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "productform";
            this.Text = "productform";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label price;
        private System.Windows.Forms.Label des;
        private System.Windows.Forms.Label quantity;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtprice;
        private System.Windows.Forms.RichTextBox txtdes;
        private System.Windows.Forms.TextBox txtquantity;
        private System.Windows.Forms.CheckedListBox checkedListcate;
        private System.Windows.Forms.Label category;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button add;
    }
}