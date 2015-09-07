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
using Imogen.Controllers.Database;
using Imogen.Controllers.Utils;
using Imogen.Forms.Dialog;

namespace Imogen.Forms.Dockable
{
    public partial class FrmProfileImage : DockContent
    {

        private Utils utils = new Utils();

        private delegate void UpdateTextBoxTextDelegate(TextBox tb, string txt);
        private void UpdateTextBoxText(TextBox tb, string txt)
        {
            if (tb.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                tb.Invoke(new UpdateTextBoxTextDelegate(this.UpdateTextBoxText), tb, txt);
            }
            else
            {
                // This is the UI thread so perform the task.

                tb.Text = txt.ToString();
            }
        }

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
            SuspendLayout();
            if (e.PropertyName == "ProfileUrlHash")
                UpdateLabelText(lblUrlHash, Properties.Settings.Default.ProfileUrlHash);
            else if (e.PropertyName == "ProfileSrcUrlHash")
                UpdateLabelText(lblSrcUrlHash, Properties.Settings.Default.ProfileSrcUrlHash);
            else if (e.PropertyName == "ProfileUrl")
                UpdateTextBoxText(lblUrl, Properties.Settings.Default.ProfileUrl);
            else if (e.PropertyName == "ProfileSrcUrl")
                UpdateTextBoxText(tbSrcUrl, Properties.Settings.Default.ProfileSrcUrl);
            else if (e.PropertyName == "ProfileLinkUrl")
                UpdateTextBoxText(tbLinkUrl, Properties.Settings.Default.ProfileLinkUrl);
            else if (e.PropertyName == "ProfileLinkUrlHash")
                UpdateLabelText(lblLinkUrlHash, Properties.Settings.Default.ProfileLinkUrlHash);
            else if (e.PropertyName == "ProfileReportedOn")
            {
                DateTime dtn = Convert.ToDateTime(Properties.Settings.Default.ProfileReportedOn);
                UpdateLabelText(lblReportedon, Properties.Settings.Default.ProfileReportedOn + " (" + utils.HowLongAgo(dtn) + ")");
            }
            else if (e.PropertyName == "ProfileReportNumber")
            {
               
                UpdateLabelText(lblReportNumber, Properties.Settings.Default.ProfileReportNumber);
              
                    // Check to see if any of the ARC settings have already been produced by other contributors
                    string r = Properties.Settings.Default.ProfileReportNumber.Replace(",", "").Trim();
                    int reportId = Convert.ToInt32(r); //convert the report Id string to int for the DBHelper
                    string srcUrlARCRating = DBHelper.GetSrcARCRating(reportId);
                    Properties.Settings.Default.ProfileLinkUrlARCRating = DBHelper.GetLinkARCValue(reportId);
               
            }
            else if (e.PropertyName == "ProfilePossibleFileName1")
                UpdateLabelText(lblPossibleFileName1, Properties.Settings.Default.ProfilePossibleFileName1);
            else if (e.PropertyName == "ProfilePossibleFileName2")
                UpdateLabelText(lblPossibleFileName2, Properties.Settings.Default.ProfilePossibleFileName2);
            else if (e.PropertyName == "ProfileLinkUrlARCRating")
                UpdateLabelText(lblLinkUrlARCRating, Properties.Settings.Default.ProfileLinkUrlARCRating);
            ResumeLayout();
        }

        internal void SetImage(string imgPath)
        {
            SuspendLayout();
            UpdatePbOriginalImage(pbOriginal, Image.FromFile(imgPath));
            Properties.Settings.Default.ImagePath = imgPath;
            ResumeLayout();
        }

        internal void ShowImage(string imgPath)
        {
            SetImage(imgPath);
        }

        private void btnAllowed_Click(object sender, EventArgs e)
        {
            lblSrcUrlARCRating.ForeColor = Color.PaleGreen;
            lblSrcUrlARCRating.Text = "Universally Allowed in this Jurisdiction";
            btnSrcSubmit.Visible = true;
            btnSrcSubmit.Tag = "A";
        }

        private void btnRestricted_Click(object sender, EventArgs e)
        {
            // Because restricted files can be miss-defined as criminal we need to do more processing than in the case of allowed files.
           

            //TODO: Who is this restricted to?
            btnSrcSubmit.Visible = true;
            btnSrcSubmit.Tag = "R";
            lblSrcUrlARCRating.ForeColor = Color.Orange;
            lblSrcUrlARCRating.Text = "Restricted in this Jurisdiction";
        }

        private void btnCriminal_Click(object sender, EventArgs e)
        {
            btnSrcSubmit.Visible = true;
            btnSrcSubmit.Tag = "C";
            lblSrcUrlARCRating.ForeColor = Color.OrangeRed;
            lblSrcUrlARCRating.Text = "Rated as Criminal Content in this Jurisdiction";
        }

        private void btnSrcUrlNoLongerAvailable_Click(object sender, EventArgs e)
        {
            btnSrcSubmit.Tag = "404";
            btnSrcSubmit.Visible = true;
            lblSrcUrlARCRating.ForeColor = Color.Yellow;
            lblSrcUrlARCRating.Text = "Source is no longer Available";
        }

        private void btnLinkUrlNoLongerAvailable_Click(object sender, EventArgs e)
        {
            lblLinkUrlARCRating.ForeColor = Color.Yellow;
            lblLinkUrlARCRating.Text = "Link is no longer Available";
            btnLinkSubmit.Visible = true;
            btnLinkSubmit.Tag = "404";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblLinkUrlARCRating.ForeColor = Color.PaleGreen;
            lblLinkUrlARCRating.Text = "Universally Allowed in this Jurisdiction";
            btnLinkSubmit.Visible = true;
            btnLinkSubmit.Tag = "A";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: Who is this restricted to?
            btnLinkSubmit.Visible = true;
            lblLinkUrlARCRating.ForeColor = Color.Orange;
            lblLinkUrlARCRating.Text = "Restricted in this Jurisdiction";
            btnLinkSubmit.Tag = "R";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnLinkSubmit.Visible = true;
            btnLinkSubmit.Tag = "C";
            lblLinkUrlARCRating.ForeColor = Color.OrangeRed;
            lblLinkUrlARCRating.Text = "Rated as Criminal Content in this Jurisdiction";
        }

        private void btnSrcSubmit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SrcSaved = true;
            btnSrcSubmit.Visible = false;
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnSrcSubmit.Tag)
            {
                case "404":                    
                    dbHelper.SetSrcToGoneButNotForgotten(Properties.Settings.Default.ProfileSrcUrl);
                    break;
                case "A":
                    dbHelper.SetSrcToAllowed(Properties.Settings.Default.ProfileSrcUrl);
                    break;
                case "R":
                    dbHelper.SetSrcToRestricted(Properties.Settings.Default.ProfileSrcUrl);
                    break;
                case "C":
                    dbHelper.SetSrcToCriminal(Properties.Settings.Default.ProfileSrcUrl);
                    break;
                default:
                    break;
            }

            HashingHelper hh = new HashingHelper();
           string fMd5 = hh.GetFileMD5(Properties.Settings.Default.FileSavePath);
            dbHelper.SaveMD5Hash(fMd5);

            string fSha1 = hh.GetFileSha1(Properties.Settings.Default.FileSavePath);
            dbHelper.SaveSha1Hash(fSha1);

            string fSha256 = hh.GetFileSha256(Properties.Settings.Default.FileSavePath);
            dbHelper.SaveSha256Hash(fSha256);

            string fSha512 = hh.GetFileSha512(Properties.Settings.Default.FileSavePath);
            dbHelper.SaveSha512Hash(fSha512);

            btnSrcSubmit.Tag = null;
            //TODO: Make Disposable?
            dbHelper = null;
        }

        private void btnLinkSubmit_Click(object sender, EventArgs e)
        {
            btnLinkSubmit.Visible = false;
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnLinkSubmit.Tag)
            {
                case "404":
                    dbHelper.SetLinkToGoneButNotForgotten(Properties.Settings.Default.ProfileLinkUrl);
                    break;
                case "A":
                    dbHelper.SetLinkToAllowed(Properties.Settings.Default.ProfileLinkUrl);
                    break;
                case "R":
                    dbHelper.SetLinkToRestricted(Properties.Settings.Default.ProfileLinkUrl);
                    break;
                case "C":
                    dbHelper.SetLinkToCriminal(Properties.Settings.Default.ProfileLinkUrl);
                    break;
                default:
                    break;
            }

            //TODO: Make Disposable?
            dbHelper = null;
        }
    }
}
