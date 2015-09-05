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



namespace Imogen.Forms.Dialog
{
    public partial class FrmVideoLinkTester : Form
    {

        /*
        <div id="player-config" style="display: none" video-id="2071306" video-title="Teen Girlfriend Makes Mobile Stripping Vivdeo" partner-url="" is-premium="0" config-final-url="http://cdn1.videolb2.pornxs.com/01052014/2071306.mp4?nvb=20150904082509&amp;nva=20150906082509&amp;hash=06e017eb6a25b69c81274" config-thumb-url="http://thumbs.pornxs.com/2014/05/01/thumbs/20713066.jpg" config-sprite-url="http://sprites.pornxs.com/vtt/01052014/2071306.vtt" href="http://cdn1.videolb2.pornxs.com/01052014/2071306.mp4?nvb=20150904082509&amp;nva=20150906082509&amp;hash=06e017eb6a25b69c81274"></div>
        */
        public FrmVideoLinkTester()
        {
            InitializeComponent();
            
        }

        public bool DownloadVideo()
        {
            string sUrl = tbSrcUrl.Text.Trim();
            webBrowser1.Navigate(sUrl);
            return true;
        }

        private void btnTry_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbSrcUrl.Text))
            {
                btnTry.Enabled = false;
                DownloadVideo();
            }
        }
    }
}
