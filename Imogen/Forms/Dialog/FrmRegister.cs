using Imogen.Controllers.Database;
using Imogen.Controllers.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imogen.Forms.Dialog
{
    public partial class FrmRegister : Form
    {
        DBHelper dbHelper = new DBHelper();

        public FrmRegister()
        {
            InitializeComponent();
           
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (userPasswordTextBox.Text == tbRepeatPassword.Text)
                {
                    if (!dbHelper.RegisterUser(usernameTextBox.Text, userPasswordTextBox.Text, emailAddressTextBox.Text, userDisplayNameTextBox.Text))
                    {
                        MessageBox.Show("Failed to register"); // TODO: Requires better information than this.
                    }
                    else // Successfully Registered
                    {
                        CurrentUser.Username = userDisplayNameTextBox.Text;
                        CurrentUser.UserPassword = userPasswordTextBox.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords Do not Match.");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            usernameTextBox.Text = CurrentUser.Username;
            ResumeLayout();
        }
    }
}
