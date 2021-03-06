﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Imogen.Controllers.Security;
using Imogen.Controllers.Database;
using Imogen.Controllers.Reporting;

namespace Imogen.Forms.Dialog
{
    public partial class FrmLogin : Form
    {
        SecurityHelper sh = new SecurityHelper();
        DBHelper dbHelper = new DBHelper();

        public FrmLogin()
        {
            InitializeComponent();          
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            cbRememberMe.Checked = CurrentUser.RememberMe;
            Text = "Login " + sh.GetUser();
            tbUsername.Text = sh.GetUser();
            ResumeLayout();
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRememberMe.Checked)
            {
                CurrentUser.RememberMe = true;
                CurrentUser.Username = sh.GetUser();
                CurrentUser.UserPassword = tbPassword.Text;
            }
            else
                CurrentUser.RememberMe = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(tbPassword.Text))
            {
                Login();
            }
            else
            {
                MessageBox.Show("A password is Required");
                btnLogin.Enabled = true;
            }
        }

        private void Login()
        {
            if (loginSuccessful())
            {
                if (CurrentUser.RememberMe)
                {
                    CurrentUser.Username = sh.GetUser();
                    CurrentUser.UserPassword = tbPassword.Text;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void LoginUsingProperties()
        {
            if (loginSuccessful())
            {
                if (CurrentUser.RememberMe)
                {
                    CurrentUser.Username = sh.GetUser();
                    CurrentUser.UserPassword = tbPassword.Text;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool loginSuccessful()
        {
            return dbHelper.Login(CurrentUser.Username, CurrentUser.UserPassword);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister register = new FrmRegister();
            DialogResult dr = register.ShowDialog();
            if (dr != DialogResult.Cancel)
            {
                // Successfully registered - so log in... 
                LoginUsingProperties();
            }
            else
            {
                // User cancelled for some reason
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
