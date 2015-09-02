using System;

namespace Imogen.Forms.Dockable
{
    partial class FrmOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutput));
            this.rtbConOut = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // rtbConOut
            // 
            this.rtbConOut.AcceptsReturn = false;
            this.rtbConOut.AcceptsTab = false;
            this.rtbConOut.AllowSeveralTextStyleDrawing = true;
            this.rtbConOut.AutoIndent = false;
            this.rtbConOut.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.rtbConOut.BackBrush = null;
            this.rtbConOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.rtbConOut.CaretColor = System.Drawing.Color.CornflowerBlue;
            this.rtbConOut.CaretVisible = false;
            this.rtbConOut.CurrentLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.rtbConOut.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbConOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConOut.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rtbConOut.IndentBackColor = System.Drawing.Color.Black;
            this.rtbConOut.Location = new System.Drawing.Point(0, 0);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.Paddings = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.rtbConOut.ReadOnly = true;
            this.rtbConOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtbConOut.ServiceLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.rtbConOut.ShowLineNumbers = false;
            this.rtbConOut.Size = new System.Drawing.Size(569, 145);
            this.rtbConOut.TabIndex = 0;
            // 
            // frmOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 145);
            this.Controls.Add(this.rtbConOut);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOutput";
            this.Text = "Output";
            this.Load += new System.EventHandler(this.frmOutput_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox rtbConOut;

       
    }
}