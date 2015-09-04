using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentReport
{
    public class Report
    {
        public const string RestrictedBrowserUserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        public string PageUrlHash { get; set; }
        public string PageUrl { get; set; }
        public string SrcUrlHash { get; set; }
        public string SrcUrl { get; set; }        
        public string LinkUrlHash { get; set; }
        public string LinkUrl { get; set; }
        public string ReportedOn { get; set; }
        public string ReportNumber { get; set; }
        public string SrcFileName { get; set; }
        public string LinkFileName { get; set; }
        public string SaveFilePath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public double SessionSeconds { get; set; }
        public double TotalSessionSeconds { get; set; }

        public void SaveSettings()
        {
            FileStream fs = new FileStream(Path.Combine(SaveFilePath, "settings.cfg"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(PageUrlHash);
            sw.WriteLine(PageUrl);
            sw.WriteLine(SrcUrlHash);
            sw.WriteLine(SrcUrl);
            sw.WriteLine(LinkUrlHash);
            sw.WriteLine(ReportedOn);
            sw.WriteLine(ReportNumber);
          //  sw.WriteLine()

            sw.Close();
            fs.Close();
        }

        public void LoadSettings()
        {
            string cfgPath = Path.Combine(SaveFilePath, "settings.cfg");

            if (File.Exists(cfgPath)) 
                {
                FileStream fs = new FileStream(cfgPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs);

                sr.Close();
                fs.Close();
            }          
        }




    }
}
