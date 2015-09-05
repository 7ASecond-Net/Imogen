﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Imogen.Forms.Dockable;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Imogen.Controllers.Database;
using Imogen.Model;
using Imogen.Controllers.Downloader;
using Imogen.Controllers.Utils;
using Imogen.Forms.Dialog;
using System.Runtime.CompilerServices;

namespace Imogen
{
    public partial class frmMain : Form
    {
        UnProcessedContent upc = new UnProcessedContent();
        Utils utils = new Utils();

        private bool bSaveLayout = true;
        private string currentFilePath = string.Empty;
        private bool ApplicationClosing = false;

        private DeserializeDockContent m_deserializeDockContent;
        private FrmOutput frmOutput = new FrmOutput();
        private DBHelper dbHelper = new DBHelper();
        private FrmProfileImage frmProfileImage = new FrmProfileImage();
        private FrmRestrictedWebBrowser frmRWB = new FrmRestrictedWebBrowser();
        private FrmMetaData frmMetadata = new FrmMetaData();
        private FrmUser frmYourAccount = new FrmUser();

        enum LogType
        {
            Information,
            Warning,
            Error,
            Success
        };

        public frmMain()
        {
            startUp();
        }

        private void startUp()
        {
            InitializeComponent();
#if DEBUG
            Properties.Settings.Default.LoginRememberMe = true;
            Properties.Settings.Default.UserUsername = Environment.MachineName + "/" + Environment.UserName;
            Properties.Settings.Default.UserPassword = "P@r1n@zK0k@b1";
#endif
            //TODO: Login
            if (Properties.Settings.Default.LoginRememberMe)
            {
                dbHelper.Login(Properties.Settings.Default.UserUsername, Properties.Settings.Default.UserPassword);
                m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            }
            else
            {
                FrmLogin frmLogin = new FrmLogin();
                DialogResult dr = frmLogin.ShowDialog();


                if (dr == DialogResult.OK)
                {
                    m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                }
                else
                {
                    DialogResult mb = MessageBox.Show("You must be Logged in to use this program." + Environment.NewLine + "Do you want to try again?", "Anonymous Usage is Forbidden", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                    if (mb != DialogResult.Cancel)
                        startUp();
                    else
                    {
                        ApplicationClosing = true;

                    }
                }
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FrmOutput).ToString())
            {
                outputToolStripMenuItem.Checked = true;
                return frmOutput;
            }
            else if (persistString == typeof(FrmProfileImage).ToString())
            {
                profileToolStripMenuItem.Checked = true;
                return frmProfileImage;
            }
            else if (persistString == typeof(FrmRestrictedWebBrowser).ToString())
            {
                restrictedBrowserToolStripMenuItem.Checked = true;
                return frmRWB;
            }
            //TODO: Not working properly
            else if (persistString == typeof(FrmMetaData).ToString())
            {
                metaDataToolStripMenuItem.Checked = true;
                return frmMetadata;
            }
            else if (persistString == typeof(FrmUser).ToString())
            {
                yourAccountToolStripMenuItem.Checked = true;
                return frmYourAccount;
            }

            return null;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!ApplicationClosing)
            {
                dockPanel.SuspendLayout();
                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

                if (File.Exists(configFile))
                    dockPanel.LoadFromXml(configFile, m_deserializeDockContent);

                lblPendingReports.Text = DBHelper.GetPendingReportCount();
                lblUsersOnline.Text = DBHelper.GetUsersOnlineCount();
                dockPanel.ResumeLayout();

                Task tStart = new Task(() => Start());
                tStart.Start();
            }
            else
                Application.Exit();
        }

        private void Start()
        {

            System.Threading.Thread.Sleep(2000);
            Log("Spinning Up");

            if (frmRWB != null)
            {
                Log("Registering to Console Messaging event");
                   frmRWB.ConsoleMessageEvent += FrmRWB_ConsoleMessageEvent;
            }

            try
            {
                System.Threading.Thread.Sleep(2000);
                ConnectToDatabase();
            }
            catch (Exception)
            {

                throw;
            }

            //TODO: Quite often does not proceed from here - Seems to now be working after frmOutput was given try catches, which are not thrown! ??


            //TODO: Don't Do this if the User is already working on a report.
            // Get Unprocessed Reported Content
            Log("Getting Unprocessed Report");
            ProcessUnReported(upc.GetUnProcessedContent());
        }

        private void ConnectToDatabase()
        {
            Log("Connecting To Damocles 2");
            // Connect to Database 
            //TODO: Change login settings so that it uses the actual user's account details and not a generic one
            if (dbHelper == null)
            {
                Log("Creating New Database Helper");
                dbHelper = new DBHelper();
            }

            if (dbHelper.DBConnected)
            {
                Log("Connected to Damocles", LogType.Success);
                dbStatusIcon.Image = Properties.Resources.accept_database_16;
                lblPendingReports.Text = DBHelper.GetPendingReportCount();
            }
            else
            {
                dbStatusIcon.Image = Properties.Resources.delete_database_16;
                Log("Failed to connect to Damocles!", LogType.Error);
            }
            Log("Database Connection Completed");
        }

        //TODO: Should probably be extracted to a separate class

        /// <summary>
        /// Process the first most important Unprocessed Report
        /// </summary>
        /// <param name="eur">
        /// Class: EUReported record
        /// </param>        
        private void ProcessUnReported(EUReported eur)
        {
            DownloadImage di = new DownloadImage();
            Log("Loading Processing of New Report"); // This could be a partial report so check the other tables as well !!
            Properties.Settings.Default.ProfileUrlHash = eur.PageUrlHash;
            Properties.Settings.Default.ProfileUrl = eur.PageUrl;
            Properties.Settings.Default.ProfileSrcUrlHash = eur.SrcUrlHash;
            Properties.Settings.Default.ProfileSrcUrl = eur.SrcUrl;


            Properties.Settings.Default.ProfileLinkUrlHash = eur.LinkUrlHash;
            Properties.Settings.Default.ProfileLinkUrl = eur.LinkUrl;
            Properties.Settings.Default.ProfileReportNumber = eur.id.ToString("N0");
            Properties.Settings.Default.ProfileReportedOn = eur.CreatedOn.ToString();
            Properties.Settings.Default.ProfilePossibleFileName1 = utils.GetPossibleFileName(eur.SrcUrl);
            Properties.Settings.Default.ProfilePossibleFileName2 = utils.GetPossibleFileName(eur.LinkUrl);
            Log("Downloading Image");
            string imgPath = di.Download(eur.SrcUrl);
            if (imgPath != null)
            {
                Log("Download Completed", LogType.Success);
                frmProfileImage.ShowImage(imgPath);
                currentFilePath = imgPath;
            }
            else
                Log("Download Failed", LogType.Error);

            // Shows the Src Image and Metadata
            Log("Opening Restricted Web Browser for Src Url");
            try
            {
                if (frmRWB == null)
                    frmRWB.Show(dockPanel, DockState.Document);
                frmRWB.ShowSrcUrl(eur.SrcUrl);
                if (frmMetadata == null)
                    frmMetadata.Show(dockPanel, DockState.DockRight);
                frmMetadata.GetMetaData(currentFilePath);
            }
            catch (Exception ex)
            {
                Log(ex.Message, LogType.Error);
                throw;
            }
        }

        #region Logging

        private void Log(string v, LogType lt = LogType.Information, [CallerMemberName]string memberName = "")
        {
            switch (lt)
            {
                case LogType.Information:
                    frmOutput.SetInformationMessage("[" + memberName + "]\t" + v);
                    break;
                case LogType.Warning:
                    break;
                case LogType.Error:
                    frmOutput.SetErrorMessage("[" + memberName + "]\t" + v);
                    break;
                case LogType.Success:
                    frmOutput.SetSuccessMessage("[" + memberName + "]\t" + v);
                    break;
                default:
                    frmOutput.SetInformationMessage("[" + memberName + "]\t" + v);
                    break;
            }
        }

        private void FrmRWB_ConsoleMessageEvent(object sender, EventArgs e)
        {
            CefSharp.ConsoleMessageEventArgs t = e as CefSharp.ConsoleMessageEventArgs;
            Log(t.Line + ": " + t.Source + ": " + t.Message);
        }
        #endregion

        #region Menu

        #region Reports
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Getting Unprocessed Report");
            if (!frmYourAccount.IsDisposed)
                frmYourAccount.UpdateUserStats();
            ProcessUnReported(upc.GetUnProcessedContent());
        }
        #endregion

        #region Windows
        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (profileToolStripMenuItem.Checked)
            {
                Log("Closing Profile Window");
                frmProfileImage.Close();
            }
            else
            {
                Log("Opening Profile Window");
                if (frmProfileImage == null || frmProfileImage.IsDisposed) frmProfileImage = new FrmProfileImage();
                frmProfileImage.Show(dockPanel);
            }

            profileToolStripMenuItem.Checked = !profileToolStripMenuItem.Checked; // Toggle the check state in the menu on click
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outputToolStripMenuItem.Checked)
            {
                Log("Closing Output Window");
                frmOutput.Close();
            }
            else
            {
                Log("Opening Output Window");
                if (frmOutput == null || frmOutput.IsDisposed) frmOutput = new FrmOutput();
                frmOutput.Show(dockPanel);
            }

            outputToolStripMenuItem.Checked = !outputToolStripMenuItem.Checked; // Toggle the check state in the menu on click
        }

        private void metaDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (metaDataToolStripMenuItem.Checked)
            {
                try
                {
                    Log("Closing Metadata Window");
                    if (frmMetadata == null || frmMetadata.IsDisposed) frmMetadata.Close();
                }
                catch (Exception)
                {

                }

            }
            else
            {
                Log("Opening Metadata Window");
                if (frmMetadata == null || frmMetadata.IsDisposed) frmMetadata = new FrmMetaData(currentFilePath);
                frmMetadata.Show(dockPanel, DockState.DockRight);
            }

            metaDataToolStripMenuItem.Checked = !metaDataToolStripMenuItem.Checked; // Toggle the check state in 
        }

        private void yourAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yourAccountToolStripMenuItem.Checked)
            {
                try
                {
                    Log("Closing Metadata Window");
                    if (frmYourAccount == null || frmYourAccount.IsDisposed) frmYourAccount.Close();
                }
                catch (Exception)
                {

                }

            }
            else
            {
                Log("Opening Metadata Window");
                if (frmYourAccount == null || frmYourAccount.IsDisposed) frmYourAccount = new FrmUser();
                frmYourAccount.Show(dockPanel, DockState.DockRight);
            }

            yourAccountToolStripMenuItem.Checked = !yourAccountToolStripMenuItem.Checked; // Toggle the check state in 
        }

        private void restrictedBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (restrictedBrowserToolStripMenuItem.Checked)
            {
                Log("Closing Restricted Browser");
                frmRWB.ConsoleMessageEvent -= FrmRWB_ConsoleMessageEvent;
                frmRWB.Close();
            }
            else
            {
                Log("Opening Restricted Browser");
                if (frmRWB == null || frmRWB.IsDisposed) frmRWB = new FrmRestrictedWebBrowser();
                frmRWB.Show(dockPanel);
                frmRWB.ConsoleMessageEvent += FrmRWB_ConsoleMessageEvent;
            }
            restrictedBrowserToolStripMenuItem.Checked = !restrictedBrowserToolStripMenuItem.Checked; // Toggle the check state in the menu on click
        }
        #endregion

        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbHelper.LogOff();
            Properties.Settings.Default.Save();
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);

        }

        private void timerDBConnect_Tick(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Connecting to Database") lblStatus.Text = string.Empty;
            if (!dbHelper.DBConnected)
            {
                dbStatusIcon.Image = Properties.Resources.delete_database_16;
                lblStatus.Text = "Connecting to Database";
                dbHelper.Connect();
                timerDBConnect.Enabled = false;
            }
            else
                dbStatusIcon.Image = Properties.Resources.accept_database_16;
        }

        private void timerFiveMinuteUpdate_Tick(object sender, EventArgs e)
        {
            lblPendingReports.Text = DBHelper.GetPendingReportCount();
            lblUsersOnline.Text = DBHelper.GetUsersOnlineCount();
        }

        private void videoLinkTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVideoLinkTester frmVideo = new FrmVideoLinkTester();
            frmVideo.ShowDialog();
        }
    }
}
