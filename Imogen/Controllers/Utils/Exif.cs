using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imogen.Controllers.Utils
{
    class Exif
    {
        internal ArrayList GetExif(string filePath)
        {
            StringBuilder sbo = new StringBuilder();

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.CreateNoWindow = true;
            string pathPartOne = Application.StartupPath;
            string fPath = Path.Combine(pathPartOne, "binn\\ExifTool\\et.exe");
            psi.FileName = fPath;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.Arguments = filePath;
            p.StartInfo = psi;
            p.Start();

            StreamReader outputStreamReader = p.StandardOutput;
            // Read the standard output of the spawned process.
            while (!outputStreamReader.EndOfStream)
            {
                string output = outputStreamReader.ReadLine();
                sbo.AppendLine(output);
            }
            p.WaitForExit();
            p.Close();

            return stringBuilderToArrayList(sbo);

        }

        private ArrayList stringBuilderToArrayList(StringBuilder sbo)
        {
            ArrayList alTmp = new ArrayList();
            foreach (string line in sbo.ToString().Split('\r'))
            {
                alTmp.Add(line.Trim('\n'));
            }
            return alTmp;
        }

    }
}

