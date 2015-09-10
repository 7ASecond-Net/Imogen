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
using Imogen.Controllers.Reporting;

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
        }

    
        internal void SetImage(string imgPath)
        {
            SuspendLayout();
            UpdatePbOriginalImage(pbOriginal, Image.FromFile(imgPath));
            CurrentInternalReport.ImagePath = imgPath;
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
            btnSrcSubmit.Tag = "X";
            btnSrcSubmit.Visible = true;
            lblSrcUrlARCRating.ForeColor = Color.Yellow;
            lblSrcUrlARCRating.Text = "Source is no longer Available";
        }

        private void btnLinkUrlNoLongerAvailable_Click(object sender, EventArgs e)
        {
            lblLinkUrlARCRating.ForeColor = Color.Yellow;
            lblLinkUrlARCRating.Text = "Link is no longer Available";
            btnLinkSubmit.Visible = true;
            btnLinkSubmit.Tag = "X";
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
            CurrentInternalReport.SrcSaved = true;
            btnSrcSubmit.Visible = false;
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnSrcSubmit.Tag)
            {
                case "X":
                    dbHelper.SetSrcToGoneButNotForgotten(CurrentInternalReport.SrcUrl);
                    break;
                case "A":
                    dbHelper.SetSrcToAllowed(CurrentInternalReport.SrcUrl);
                    break;
                case "R":
                    dbHelper.SetSrcToRestricted(CurrentInternalReport.SrcUrl);
                    break;
                case "C":
                    dbHelper.SetSrcToCriminal(CurrentInternalReport.SrcUrl);
                    break;
                default:
                    break;
            }

            // Save the Hashes

            HashingHelper hh = new HashingHelper();
            string fMd5 = hh.GetFileMD5(CurrentInternalReport.ImagePath);
            dbHelper.SaveMD5Hash(fMd5);

            string fSha1 = hh.GetFileSha1(CurrentInternalReport.ImagePath);
            dbHelper.SaveSha1Hash(fSha1);

            string fSha256 = hh.GetFileSha256(CurrentInternalReport.ImagePath);
            dbHelper.SaveSha256Hash(fSha256);

            string fSha512 = hh.GetFileSha512(CurrentInternalReport.ImagePath);
            dbHelper.SaveSha512Hash(fSha512);

            // Now we need to save the Metadata!



            btnSrcSubmit.Tag = null;
            //TODO: Make Disposable?
            dbHelper = null;
        }

        private void btnLinkSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTrueDestinationLinkUrl.Text))
            {
                HashingHelper hh = new HashingHelper();
                CurrentInternalReport.TrueLinkUrl = tbTrueDestinationLinkUrl.Text;
                CurrentInternalReport.TrueLinkUrlHash = hh.GetSHA512(tbTrueDestinationLinkUrl.Text);
                hh = null;
            }


            btnLinkSubmit.Visible = false;
            DBHelper dbHelper = new DBHelper();
            switch ((string)btnLinkSubmit.Tag)
            {
                case "X":
                    dbHelper.SetLinkToGoneButNotForgotten(CurrentInternalReport.LinkUrl);
                    break;
                case "A":
                    dbHelper.SetLinkToAllowed(CurrentInternalReport.LinkUrl);
                    break;
                case "R":
                    dbHelper.SetLinkToRestricted(CurrentInternalReport.LinkUrl);
                    break;
                case "C":
                    dbHelper.SetLinkToCriminal(CurrentInternalReport.LinkUrl);
                    break;
                default:
                    break;
            }

            //TODO: Make Disposable?
            dbHelper = null;
        }
    }
}
