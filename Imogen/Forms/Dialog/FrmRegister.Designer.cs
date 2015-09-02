namespace Imogen.Forms.Dialog
{
    partial class FrmRegister
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label userDisplayNameLabel;
            System.Windows.Forms.Label emailAddressLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label userPasswordLabel;
            System.Windows.Forms.Label usernameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegister));
            this.userDisplayNameTextBox = new System.Windows.Forms.TextBox();
            this.eUReportedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.emailAddressTextBox = new System.Windows.Forms.TextBox();
            this.tbRepeatPassword = new System.Windows.Forms.TextBox();
            this.userPasswordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            userDisplayNameLabel = new System.Windows.Forms.Label();
            emailAddressLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            userPasswordLabel = new System.Windows.Forms.Label();
            usernameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eUReportedBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // userDisplayNameLabel
            // 
            userDisplayNameLabel.AutoSize = true;
            userDisplayNameLabel.Location = new System.Drawing.Point(9, 40);
            userDisplayNameLabel.Name = "userDisplayNameLabel";
            userDisplayNameLabel.Size = new System.Drawing.Size(100, 13);
            userDisplayNameLabel.TabIndex = 40;
            userDisplayNameLabel.Text = "User Display Name:";
            // 
            // emailAddressLabel
            // 
            emailAddressLabel.AutoSize = true;
            emailAddressLabel.Location = new System.Drawing.Point(33, 115);
            emailAddressLabel.Name = "emailAddressLabel";
            emailAddressLabel.Size = new System.Drawing.Size(76, 13);
            emailAddressLabel.TabIndex = 27;
            emailAddressLabel.Text = "Email Address:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 90);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 13);
            label1.TabIndex = 25;
            label1.Text = "Repeat Password:";
            // 
            // userPasswordLabel
            // 
            userPasswordLabel.AutoSize = true;
            userPasswordLabel.Location = new System.Drawing.Point(28, 65);
            userPasswordLabel.Name = "userPasswordLabel";
            userPasswordLabel.Size = new System.Drawing.Size(81, 13);
            userPasswordLabel.TabIndex = 23;
            userPasswordLabel.Text = "User Password:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new System.Drawing.Point(51, 15);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new System.Drawing.Size(58, 13);
            usernameLabel.TabIndex = 21;
            usernameLabel.Text = "Username:";
            // 
            // userDisplayNameTextBox
            // 
            this.userDisplayNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eUReportedBindingSource, "User.UserDisplayName", true));
            this.userDisplayNameTextBox.Location = new System.Drawing.Point(115, 37);
            this.userDisplayNameTextBox.Name = "userDisplayNameTextBox";
            this.userDisplayNameTextBox.Size = new System.Drawing.Size(220, 20);
            this.userDisplayNameTextBox.TabIndex = 41;
            // 
            // eUReportedBindingSource
            // 
            this.eUReportedBindingSource.DataSource = typeof(Imogen.Model.EUReported);
            // 
            // emailAddressTextBox
            // 
            this.emailAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eUReportedBindingSource, "User.EmailAddress", true));
            this.emailAddressTextBox.Location = new System.Drawing.Point(115, 112);
            this.emailAddressTextBox.Name = "emailAddressTextBox";
            this.emailAddressTextBox.Size = new System.Drawing.Size(220, 20);
            this.emailAddressTextBox.TabIndex = 28;
            // 
            // tbRepeatPassword
            // 
            this.tbRepeatPassword.Location = new System.Drawing.Point(115, 87);
            this.tbRepeatPassword.Name = "tbRepeatPassword";
            this.tbRepeatPassword.Size = new System.Drawing.Size(220, 20);
            this.tbRepeatPassword.TabIndex = 26;
            this.tbRepeatPassword.UseSystemPasswordChar = true;
            // 
            // userPasswordTextBox
            // 
            this.userPasswordTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eUReportedBindingSource, "User.UserPassword", true));
            this.userPasswordTextBox.Location = new System.Drawing.Point(115, 62);
            this.userPasswordTextBox.Name = "userPasswordTextBox";
            this.userPasswordTextBox.Size = new System.Drawing.Size(220, 20);
            this.userPasswordTextBox.TabIndex = 24;
            this.userPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eUReportedBindingSource, "User.Username", true));
            this.usernameTextBox.Location = new System.Drawing.Point(115, 12);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.ReadOnly = true;
            this.usernameTextBox.Size = new System.Drawing.Size(220, 20);
            this.usernameTextBox.TabIndex = 22;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(260, 138);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 42;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(115, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmRegister
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(347, 168);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(userDisplayNameLabel);
            this.Controls.Add(this.userDisplayNameTextBox);
            this.Controls.Add(emailAddressLabel);
            this.Controls.Add(this.emailAddressTextBox);
            this.Controls.Add(this.tbRepeatPassword);
            this.Controls.Add(label1);
            this.Controls.Add(userPasswordLabel);
            this.Controls.Add(this.userPasswordTextBox);
            this.Controls.Add(usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.eUReportedBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource eUReportedBindingSource;
        private System.Windows.Forms.TextBox userDisplayNameTextBox;
        private System.Windows.Forms.TextBox emailAddressTextBox;
        private System.Windows.Forms.TextBox tbRepeatPassword;
        private System.Windows.Forms.TextBox userPasswordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
    }
}