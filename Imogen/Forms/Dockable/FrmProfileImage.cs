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
                UpdateLabelText(lblReportedon, Properties.Settings.Default.ProfileReportedOn);
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
            lblARCRating.ForeColor = Color.PaleGreen;
            lblARCRating.Text = "Universally Allowed in this Jurisdiction";
            btnSubmit.Enabled = true;
        }

        private void btnRestricted_Click(object sender, EventArgs e)
        {
            //TODO: Who is this restricted to?
            btnSubmit.Enabled = true;
            lblARCRating.ForeColor = Color.Orange;
            lblARCRating.Text = "Restricted in this Jurisdiction";
        }

        private void btnCriminal_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            lblARCRating.ForeColor = Color.OrangeRed;
            lblARCRating.Text = "Rated as Criminal Content in this Jurisdiction";
        }
    }
}
