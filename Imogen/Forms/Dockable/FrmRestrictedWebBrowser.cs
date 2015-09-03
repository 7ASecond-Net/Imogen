using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using CefSharp.WinForms;
using CefSharp;

namespace Imogen.Forms.Dockable
{
    public partial class FrmRestrictedWebBrowser : DockContent
    {

        private static string currentSrcUrl { get; set; }

        public event EventHandler ConsoleMessageEvent;

        CefSharp.WinForms.ChromiumWebBrowser cwb = new ChromiumWebBrowser("");

        public FrmRestrictedWebBrowser()
        {
            InitializeComponent();
            try
            {
  //webBrowser.StatusTextChanged += WebBrowser_StatusTextChanged;
            //webBrowser.ProgressChanged += WebBrowser_ProgressChanged;
            cwb.Dock = DockStyle.Fill;
            cwb.ContextMenu = null;

            cwb.ConsoleMessage += Cwb_ConsoleMessage;
            cwb.StatusMessage += Cwb_StatusMessage;
            cwb.NavStateChanged += Cwb_NavStateChanged;
            cwb.LoadError += Cwb_LoadError;

            CefSharp.BrowserSettings cbs = new CefSharp.BrowserSettings();
            cbs.ApplicationCacheDisabled = true;
            cbs.FileAccessFromFileUrlsAllowed = false;
            cbs.JavaScriptCloseWindowsDisabled = true;
            cbs.PluginsDisabled = true;
            cbs.WebSecurityDisabled = false;
            cwb.BrowserSettings = cbs;

            CefSharp.CefSettings cs = new CefSharp.CefSettings();
            cs.IgnoreCertificateErrors = true;
            cs.UserAgent = Imogen.Properties.Settings.Default.RestrictedBrowserUserAgent;
            cwb.AllowDrop = false;

            this.Controls.Add(cwb);
            }
            catch (Exception)
            {

                throw;
            }
          

        }

        private void Cwb_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            try
            {
                lblStatusCode.Text = e.ErrorCode.ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Cwb_NavStateChanged(object sender, CefSharp.NavStateChangedEventArgs e)
        {
            try
            {
                if (e.IsLoading)
                    lblStatusText.Text = "Loading";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Cwb_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
            try
            {
                lblStatusText.Text = e.Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Cwb_ConsoleMessage(object sender, CefSharp.ConsoleMessageEventArgs e)
        {
            try
            {
                if (ConsoleMessageEvent != null)
                {
                    //TODO: Need to fix this so we can send a string and not the ConsoleMessageEventArgs
                    // ConsoleMessageEvent(this, e.Line + ": " + e.Source + ": " + e.Message.ToString() );
                }
            }
            catch (Exception)
            {

                throw;
            }
            // Raise an Event for frmMain log

        }

        internal void ShowSrcUrl(string srcUrl)
        {
            try
            {
                currentSrcUrl = srcUrl;
                cwb.Load(srcUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void ShowLinkUrl(string linkUrl)
        {
            try
            {
                cwb.Load(linkUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void showSrcUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Actual View of the Src Url";
            ShowSrcUrl(currentSrcUrl);
        }

        private void showLinkUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Actual View of the Link Url";
            ShowLinkUrl(Properties.Settings.Default.ProfileLinkUrl);
        }
    }
}

