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
using NTimeAgo;
using Imogen.Controllers.Database;

namespace Imogen.Forms.Dockable
{
    public partial class FrmProfileImage : DockContent
    {
        

        private delegate void UpdateLabelTextDelegate(Label lbl, string txt);
        private void UpdateLabelText(Label lbl, string txt)
        {
            if (lbl.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                lbl.Invoke(new UpdateLabelTextDelegate(this.UpdateLabelText), lbl, txt);
            }
            else
            {
                // This is the UI thread so perform the task.

                lbl.Text = txt.ToString();
            }
        }

        private delegate void UpdatePbOriginalImageDelegate(PictureBox pb, Image img);
        private void UpdatePbOriginalImage(PictureBox pb, Image img)
        { 
            if (pb.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                pb.Invoke(new UpdatePbOriginalImageDelegate(this.UpdatePbOriginalImage), pb, img);
            }
            else
            {
                // This is the UI thread so perform the task.
                pbOriginal.Image = img;
            }
        }

        public FrmProfileImage()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ProfileUrlHash")
                UpdateLabelText(lblUrlHash, Properties.Settings.Default.ProfileUrlHash);
            else if (e.PropertyName == "ProfileSrcUrlHash")
                UpdateLabelText(lblSrcUrlHash, Properties.Settings.Default.ProfileSrcUrlHash);
            else if (e.PropertyName == "ProfileLinkUrlHash")
                UpdateLabelText(lblLinkUrlHash, Properties.Settings.Default.ProfileLinkUrlHash);
            else if (e.PropertyName == "ProfileReportedOn")
            {
                //TODO: Not providing a very accurate result
                DateTime dtn = Convert.ToDateTime(Properties.Settings.Default.ProfileReportedOn);
                TimeSpan ts = new TimeSpan(dtn.Ticks / (1000 * 360));
                UpdateLabelText(lblReportedon, Properties.Settings.Default.ProfileReportedOn + " ~(" + ts.InWordsAgo() + ")");
            }
            else if (e.PropertyName == "ProfileReportNumber")
                UpdateLabelText(lblReportNumber, Properties.Settings.Default.ProfileReportNumber);
            else if (e.PropertyName == "ProfilePossibleFileName1")
                UpdateLabelText(lblPossibleFileName1, Properties.Settings.Default.ProfilePossibleFileName1);
            else if (e.PropertyName == "ProfilePossibleFileName2")
                UpdateLabelText(lblPossibleFileName2, Properties.Settings.Default.ProfilePossibleFileName2);
        }

        internal void SetImage(string imgPath)
        {
            UpdatePbOriginalImage(pbOriginal, Image.FromFile(imgPath));
        }

        internal void ShowImage(string imgPath)
        {
            SetImage(imgPath);
        }

        private void btnAllowed_Click(object sender, EventArgs e)
        {
            lblSrcUrlARCRating.ForeColor = Color.PaleGreen;
            lblSrcUrlARCRating.Text = "Universally Allowed in this Jurisdiction";
            btnSrcSubmit.Enabled = true;
            btnSrcSubmit.Tag = "A";
        }

        private void btnRestricted_Click(object sender, EventArgs e)
        {
            //TODO: Who is this restricted to?
            btnSrcSubmit.Enabled = true;
            btnSrcSubmit.Tag = "R";
            lblSrcUrlARCRating.ForeColor = Color.Orange;
            lblSrcUrlARCRating.Text = "Restricted in this Jurisdiction";
        }

        private void btnCriminal_Click(object sender, EventArgs e)
        {
            btnSrcSubmit.Enabled = true;
            btnSrcSubmit.Tag = "C";
            lblSrcUrlARCRating.ForeColor = Color.OrangeRed;
            lblSrcUrlARCRating.Text = "Rated as Criminal Content in this Jurisdiction";
        }

        private void btnSrcUrlNoLongerAvailable_Click(object sender, EventArgs e)
        {
            btnSrcSubmit.Tag = "404";
            btnSrcSubmit.Enabled = true;
            lblSrcUrlARCRating.ForeColor = Color.Yellow;
            lblSrcUrlARCRating.Text = "Source is no longer Available";
        }

        private void btnLinkUrlNoLongerAvailable_Click(object sender, EventArgs e)
        {
            lblLinkUrlARCRating.ForeColor = Color.Yellow;
            lblLinkUrlARCRating.Text = "Link is no longer Available";
            btnLinkSubmit.Enabled = true;
            btnLinkSubmit.Tag = "404";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblLinkUrlARCRating.ForeColor = Color.PaleGreen;
            lblLinkUrlARCRating.Text = "Universally Allowed in this Jurisdiction";
            btnLinkSubmit.Enabled = true;
            btnLinkSubmit.Tag = "A";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: Who is this restricted to?
            btnLinkSubmit.Enabled = true;
            lblLinkUrlARCRating.ForeColor = Color.Orange;
            lblLinkUrlARCRating.Text = "Restricted in this Jurisdiction";
            btnLinkSubmit.Tag = "R";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnLinkSubmit.Enabled = true;
            btnLinkSubmit.Tag = "C";
            lblLinkUrlARCRating.ForeColor = Color.OrangeRed;
            lblLinkUrlARCRating.Text = "Rated as Criminal Content in this Jurisdiction";
        }

        private void btnSrcSubmit_Click(object sender, EventArgs e)
        {
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnSrcSubmit.Tag)
            {
                case "404":
                    dbHelper.SetSrcToGoneButNotForgotten((string)btnSrcSubmit.Tag);
                    break;
                case "A":
                    dbHelper.SetSrcToAllowed((string)btnSrcSubmit.Tag);
                    break;
                case "R":
                    dbHelper.SetSrcToRestricted((string)btnSrcSubmit.Tag);
                    break;
                case "C":
                    dbHelper.SetSrcToCriminal((string)btnSrcSubmit.Tag);
                    break;
                default:
                    break;
            }
            
            //TODO: Make Disposable?
            dbHelper = null;
        }

        private void btnLinkSubmit_Click(object sender, EventArgs e)
        {
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnLinkSubmit.Tag)
            {
                case "404":
                    dbHelper.SetLinkToGoneButNotForgotten((string)btnLinkSubmit.Tag);
                    break;
                case "A":
                    dbHelper.SetLinkToAllowed((string)btnLinkSubmit.Tag);
                    break;
                case "R":
                    dbHelper.SetLinkToRestricted((string)btnLinkSubmit.Tag);
                    break;
                case "C":
                    dbHelper.SetLinkToCriminal((string)btnLinkSubmit.Tag);
                    break;
                default:
                    break;
            }

            //TODO: Make Disposable?
            dbHelper = null;
        }
    }
}
