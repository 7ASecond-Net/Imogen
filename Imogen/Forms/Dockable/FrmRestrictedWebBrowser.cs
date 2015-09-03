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

        public event EventHandler ConsoleMessageEvent;

        CefSharp.WinForms.ChromiumWebBrowser cwb = new ChromiumWebBrowser("");

        public FrmRestrictedWebBrowser()
        {
            InitializeComponent();
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

        private void Cwb_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            lblStatusCode.Text = e.ErrorCode.ToString();
        }

        private void Cwb_NavStateChanged(object sender, CefSharp.NavStateChangedEventArgs e)
        {
            if (e.IsLoading)
                lblStatusText.Text = "Loading";
        }

        private void Cwb_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
            lblStatusText.Text = e.Value.ToString();
        }

        private void Cwb_ConsoleMessage(object sender, CefSharp.ConsoleMessageEventArgs e)
        {
            // Raise an Event for frmMain log
            if (ConsoleMessageEvent != null)
                ConsoleMessageEvent(this, e);
        }

        internal void ShowSrcUrl(string srcUrl)
        {
            cwb.Load(srcUrl);
        }

    }    
}

