using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace Imogen.Forms.Dockable
{
    public partial class FrmOutput : DockContent
    {
        #region Internal
        public FrmOutput()
        {
            InitializeComponent();
        }

        private void frmOutput_Load(object sender, EventArgs e)
        {
            SetInformationMessage(Application.ProductName + " version:" + GetVersion());
        }

        private string GetVersion()
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            //return fvi.FileVersion;

            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
        #endregion

        Style InformationStyle = new TextStyle(Brushes.CornflowerBlue, null, FontStyle.Regular);
        Style ErrorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        Style SuccessStyle = new TextStyle(Brushes.LawnGreen, null, FontStyle.Regular);

        #region Public Methods

        /// <summary>
        /// Display Information Messages
        /// </summary>
        /// <param name="msg">
        /// The information to display
        /// </param>
        /// <remarks>
        /// Information Messages are displayed in the default foreground colour.
        /// </remarks>
        public void SetInformationMessage(string msg)
        {
            msg = msg + Environment.NewLine;
            rtbConOut.InsertText(msg, InformationStyle);            
        }

        internal void SetErrorMessage(string msg)
        {
            msg = msg + Environment.NewLine;
            rtbConOut.InsertText(msg, ErrorStyle);
        }

        internal void SetSuccessMessage(string msg)
        {
            msg = msg + Environment.NewLine;
            rtbConOut.InsertText(msg, SuccessStyle);
        }


        #endregion
    }
}
