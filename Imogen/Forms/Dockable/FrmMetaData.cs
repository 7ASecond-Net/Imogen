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
using Imogen.Controllers.Utils;
using System.Collections;

namespace Imogen.Forms.Dockable
{
    public partial class FrmMetaData : DockContent
    {

        private delegate void UpdateGridViewDelegate(DataGridViewRow r);
        private void UpdateGridView(DataGridViewRow r)
        {
            if (dataGridViewMetaData.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                dataGridViewMetaData.Invoke(new UpdateGridViewDelegate(this.UpdateGridView), r);
            }
            else
            {
                // This is the UI thread so perform the task.

                dataGridViewMetaData.Rows.Add(r);
            }
        }

        private Exif exif;
        private ArrayList alResults = new ArrayList();

        private string lastSrcFilePath = string.Empty;

        public FrmMetaData()
        {
            InitializeComponent();

        }

        public FrmMetaData(string srcPath)
        {
            InitializeComponent();
            lastSrcFilePath = srcPath;
            Task tStart = new Task(() => GetMetaData(srcPath));
            tStart.Start();
        }

        internal void GetMetaData(string srcPath)
        {
            lastSrcFilePath = srcPath;
            exif = new Exif();
            alResults = exif.GetExif(srcPath);

            try
            {
                for (int idx = 0; idx < alResults.Count - 1; idx++) // (string row in alResults)
                {
                    DataGridViewRow r = (DataGridViewRow)dataGridViewMetaData.Rows[0].Clone();
                    r.CreateCells(dataGridViewMetaData);
                    r.Cells[0].Value = alResults[idx].ToString().Split(':')[0].ToString().Trim();
                    r.Cells[1].Value = alResults[idx].ToString().Split(':')[1].ToString().Trim();
                    UpdateGridView(r);
                }
            }
            catch (Exception ex)
            {
                string res = ex.Message;
                throw;
            }
            dataGridViewMetaData.Refresh();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastSrcFilePath))
                GetMetaData(lastSrcFilePath);
        }
    }
}
