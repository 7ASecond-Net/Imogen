using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Imogen.Controllers.Utils;

namespace Imogen.Controllers.Downloader
{
    class DownloadImage
    {

        private Utils.Utils utils = new Utils.Utils();

        /// <summary>
        /// Downloads Image from location and saves it for later analysis
        /// </summary>
        /// <param name="srcUrl">
        /// String: The source location of the Image to be downloaded
        /// </param>
        /// <returns>
        /// String: If Successful it returns the path to the image
        /// String: If UnSuccessful it returns [StatusCode]error code.
        /// </returns>
        internal string Download(string srcUrl)
        {
            
            string result = string.Empty;

            try
            {
                WebClient wc = new WebClient();
                result = utils.GetNewSrcFilename();
                wc.DownloadFile(srcUrl, result); //TODO: Ensure when finished this file is Moved to Damocles2 FileStore and this copy is DELETED
            }
            catch (Exception)
            {              
                throw;
            }          
            return result; // Path to the image
        }

    }
}
