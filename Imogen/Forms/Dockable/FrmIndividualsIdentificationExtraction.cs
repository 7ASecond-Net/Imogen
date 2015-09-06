using Imogen.Controllers.Utils;
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
using Accord.Imaging;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using Imogen.Controllers.Database;

namespace Imogen.Forms.Dialog
{
    public partial class FrmIndividualsIdentificationExtraction : DockContent
    {
        Languages langs = new Languages();
        Ethnicities ethnicities = new Ethnicities();
        Gender gender = new Gender();

        public FrmIndividualsIdentificationExtraction()
        {
            InitializeComponent();

        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ImagePath")
                LoadImage(Properties.Settings.Default.ImagePath);

        }

        private void PbOriginal_ImagePortionCopied(object sender, Tpsc.Controls.ImagePortionCopiedEventArgs e)
        {
            pbFace.Image = e.Image;
            FitFaceImage(e);
        }

        private void FitFaceImage(Tpsc.Controls.ImagePortionCopiedEventArgs e = null)
        {
            if (e == null)
            {
                if (pbFace.Image.Width > pbFace.Image.Height)
                    pbFace.FitWidth();
                else
                    pbFace.FitHeight();
            }
            else
            {
                if (pbFace.Image.Width > pbFace.Image.Height)
                    pbFace.FitWidth();
                else
                    pbFace.FitHeight();
            }
        }

        public void LoadImage(string srcPath)
        {
            if (!string.IsNullOrEmpty(srcPath))
            {
                pbOriginal.Image = Bitmap.FromFile(srcPath);
                FitOriginalImage();
            }
        }

        private void FitOriginalImage()
        {
            if (pbOriginal.Image.Width > pbOriginal.Image.Height)
                pbOriginal.FitWidth();
            else
                pbOriginal.FitHeight();
        }

        private void FrmIndividualsIdentificationExtraction_Load(object sender, EventArgs e)
        {
            SuspendLayout();   
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ImagePath))
                LoadImage(Properties.Settings.Default.ImagePath);
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            pbOriginal.ImagePortionCopied += PbOriginal_ImagePortionCopied;


            //TODO: Probably quicker caching the results of langs?
            foreach (string lang in langs.GetAvailableLanguages())
            {
                cbSpokenLanguage.Items.Add(lang);
            }

            foreach (string lang in langs.GetAvailableLanguages())
            {
                cbWrittenLanguage.Items.Add(lang);
            }

            foreach(string lang in langs.GetAvailableLanguages())
            {
                cbNationality.Items.Add(lang);
            }

            foreach (string ethnicCode in ethnicities.GetAvailableEthnicCodes())
            {
                cbEthnicityCodes.Items.Add(ethnicCode);
            }

            foreach (string sex in gender.GetAllSexes())
            {
                cbSex.Items.Add(sex);
            }
            ResumeLayout();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadImage(Properties.Settings.Default.ImagePath);
        }

        private void tbRotate_Scroll(object sender, EventArgs e)
        {
            tbScrollValue.Text = tbRotate.Value.ToString();
            RotateBicubic filter = new RotateBicubic(tbRotate.Value, true);
            //RotateNearestNeighbor filter = new RotateNearestNeighbor(tbRotate.Value, true);
            Bitmap tmpImage = new Bitmap(pbFace.Image);
            Bitmap convertedBitmap = ConvertToFortmat(tmpImage, PixelFormat.Format24bppRgb);
            Bitmap newImage = filter.Apply(convertedBitmap);
            pbFace.Image = null;
            pbFace.Image = newImage;
            convertedBitmap.Dispose();
            tmpImage.Dispose();
            FitFaceImage();
        }

        private Bitmap ConvertToFortmat(Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }

        private void tbScrollValue_TextChanged(object sender, EventArgs e)
        {
            tbRotate.Value = Convert.ToInt32(tbScrollValue.Text);
        }

        private void btnRotate90_Click(object sender, EventArgs e)
        {
            if (pbFace.Image == null)
                return;
            
            Bitmap tmpImage = new Bitmap(pbFace.Image);
            tmpImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pbFace.Image = tmpImage;
            FitFaceImage();
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (pbFace.Image == null)
                return;
            Bitmap tmpImage = new Bitmap(pbFace.Image);
            tmpImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pbFace.Image = tmpImage;
            FitFaceImage();
        }

        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            if (pbFace.Image == null)
                return;
            Bitmap tmpImage = new Bitmap(pbFace.Image);
            tmpImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pbFace.Image = tmpImage;
            FitFaceImage();
        }

       
        private void SaveChanges()
        {
            DBHelper.SaveIndividualsBasicInformation(tbName.Text, tbAge.Text, cbSex.SelectedText, cbSpokenLanguage.SelectedText, cbWrittenLanguage.SelectedText, cbNationality.SelectedText, cbEthnicityCodes.SelectedText, pbFace.Image);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
    }
}
