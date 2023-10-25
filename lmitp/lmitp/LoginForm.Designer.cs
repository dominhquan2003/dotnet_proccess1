using DevExpress.DXTemplateGallery.Extensions;
using DevExpress.XtraBars.ViewInfo;
using static DevExpress.LookAndFeel.DXSkinColors;
using System.Drawing;
using System.Windows.Forms;

namespace lmitp
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            clearButton = new Button();
            LoginButton = new Button();
            checkboxShowPass = new CheckBox();
            label2 = new Label();
            txtUsername = new TextBox();
            label1 = new Label();
            label3 = new Label();
            txtPassword = new TextBox();
            SuspendLayout();
            // 
            // clearButton
            // 
            clearButton.BackColor = Color.White;
            clearButton.Cursor = Cursors.Hand;
            clearButton.FlatAppearance.BorderSize = 2;
            clearButton.FlatStyle = FlatStyle.Flat;
            clearButton.ForeColor = Color.FromArgb(116, 86, 174);
            clearButton.Location = new Point(195, 269);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(218, 28);
            clearButton.TabIndex = 4;
            clearButton.Text = "CLEAR";
            clearButton.UseVisualStyleBackColor = false;
            clearButton.Click += clearButton_Click;
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.FromArgb(116, 86, 174);
            LoginButton.Cursor = Cursors.Hand;
            LoginButton.FlatAppearance.BorderSize = 0;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.ForeColor = Color.White;
            LoginButton.Location = new Point(171, 235);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(265, 28);
            LoginButton.TabIndex = 3;
            LoginButton.Text = "LOGIN";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // checkboxShowPass
            // 
            checkboxShowPass.AutoSize = true;
            checkboxShowPass.Cursor = Cursors.Hand;
            checkboxShowPass.FlatStyle = FlatStyle.Flat;
            checkboxShowPass.Location = new Point(405, 196);
            checkboxShowPass.Name = "checkboxShowPass";
            checkboxShowPass.Size = new Size(119, 21);
            checkboxShowPass.TabIndex = 2;
            checkboxShowPass.Text = "Show Password";
            checkboxShowPass.UseVisualStyleBackColor = true;
            checkboxShowPass.CheckedChanged += checkboxShowPass_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 148);
            label2.Name = "label2";
            label2.Size = new Size(66, 17);
            label2.TabIndex = 8;
            label2.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(230, 231, 233);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("MS UI Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtUsername.Location = new Point(129, 95);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(395, 28);
            txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 99);
            label1.Name = "label1";
            label1.Size = new Size(42, 17);
            label1.TabIndex = 9;
            label1.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("MS UI Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(116, 86, 174);
            label3.Location = new Point(195, 23);
            label3.Name = "label3";
            label3.Size = new Size(177, 27);
            label3.TabIndex = 6;
            label3.Text = "LOGIN FORM";

            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(230, 231, 233);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("MS UI Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(129, 148);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(395, 28);
            txtPassword.TabIndex = 1;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(566, 345);
            ControlBox = false;
            Controls.Add(clearButton);
            Controls.Add(LoginButton);
            Controls.Add(checkboxShowPass);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Controls.Add(label3);
            Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(164, 165, 169);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.CheckBox checkboxShowPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
    }



}
