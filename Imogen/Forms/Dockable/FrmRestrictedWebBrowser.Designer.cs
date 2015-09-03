using System;

namespace Imogen.Forms.Dockable
{
    partial class FrmRestrictedWebBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRestrictedWebBrowser));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Progress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.showLinkUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSrcUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Progress,
            this.toolStripStatusLabel1,
            this.lblStatusCode,
            this.toolStripStatusLabel3,
            this.lblStatusText,
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(796, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Progress
            // 
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(100, 18);
            this.Progress.Step = 1;
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(73, 19);
            this.toolStripStatusLabel1.Text = "Status Code:";
            // 
            // lblStatusCode
            // 
            this.lblStatusCode.Name = "lblStatusCode";
            this.lblStatusCode.Size = new System.Drawing.Size(25, 19);
            this.lblStatusCode.Text = "000";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(70, 19);
            this.toolStripStatusLabel3.Text = "Status Text:";
            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(451, 19);
            this.lblStatusText.Spring = true;
            this.lblStatusText.Text = "...";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLinkUrlToolStripMenuItem,
            this.showSrcUrlToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Imogen.Properties.Resources.show_property_16;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // showLinkUrlToolStripMenuItem
            // 
            this.showLinkUrlToolStripMenuItem.Image = global::Imogen.Properties.Resources.link_4_16;
            this.showLinkUrlToolStripMenuItem.Name = "showLinkUrlToolStripMenuItem";
            this.showLinkUrlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showLinkUrlToolStripMenuItem.Text = "Show Link Url";
            this.showLinkUrlToolStripMenuItem.Click += new System.EventHandler(this.showLinkUrlToolStripMenuItem_Click);
            // 
            // showSrcUrlToolStripMenuItem
            // 
            this.showSrcUrlToolStripMenuItem.Image = global::Imogen.Properties.Resources.check_mark_3_16;
            this.showSrcUrlToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.showSrcUrlToolStripMenuItem.Name = "showSrcUrlToolStripMenuItem";
            this.showSrcUrlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showSrcUrlToolStripMenuItem.Text = "Show Src Url";
            this.showSrcUrlToolStripMenuItem.Click += new System.EventHandler(this.showSrcUrlToolStripMenuItem_Click);
            // 
            // FrmRestrictedWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 488);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRestrictedWebBrowser";
            this.Text = "Actual View of the Src Url";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCode;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.ToolStripProgressBar Progress;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem showLinkUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSrcUrlToolStripMenuItem;
    }
}