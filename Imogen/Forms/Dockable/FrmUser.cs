﻿using Imogen.Controllers.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace Imogen.Forms.Dockable
{
    public partial class FrmUser : DockContent
    {
        DBHelper dbHelper = new DBHelper();

        private double TotalSecondsPassed = 0;
        private double TotalSessionSeconds = 0;

        public FrmUser()
        {
            InitializeComponent();           
        }

        public void UpdateUserSessionTime()
        {
            TotalSecondsPassed += dbHelper.GetUserLifetimeSessionTime();
        }

        //TODO: NOT WORKING
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            TimeSpan sessionDuration = TimeSpan.FromSeconds(TotalSessionSeconds);
            TimeSpan totalDuration = TimeSpan.FromSeconds(TotalSecondsPassed);
            lblSessionTime.Text = sessionDuration.ToString();
            lblTotalWorkTime.Text = totalDuration.ToString();

            Properties.Settings.Default.SessionSeconds = TotalSessionSeconds;

            TotalSecondsPassed ++;
            TotalSessionSeconds ++;
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Properties.Settings.Default.UserUsername;
            UpdateUserSessionTime();
        }
    }
}
