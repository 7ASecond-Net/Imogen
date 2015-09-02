using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Utils
{
    public class Utils
    {

        private HashingHelper hh = new HashingHelper();

        /// <summary>
        /// Creates a New Filename for the downloaded file.
        /// </summary>
        /// <param name="ext">
        /// string: This is the extension to use. It defaults to jpg is no extension is provided.
        /// </param>
        /// <returns>
        /// string: The New File name - with path.
        /// </returns>       
        internal string GetNewFilename(string ext = "jpg")
        {
            return Path.Combine(Properties.Settings.Default.FileSavePath, Path.ChangeExtension(Guid.NewGuid().ToString().Replace("-","").ToLowerInvariant(), ext));
        }

        /// <summary>
        /// Tries to determine the actual file name of the reported content
        /// </summary>
        /// <param name="Url">
        /// string: The string to extract the file name from
        /// </param>
        /// <returns>
        /// string: The filename discovered.
        /// </returns>
        internal string GetPossibleFileName(string Url)
        {
            // http://lh6.ggpht.com/-s4YidfA8vB8/TtnsEdTLPrI/AAAAAAAAAdY/mZQXHdYDng4/s150/c380ea0079a4ac7e59ad373f64f2b0dd.jpg - Image's Actual file name and path
            // http://myemogirl.com/out.php?t=1.0.0.3448&url=http://myemogirl.com/g.php?g=aHR0cDovL3ZpZGVhcm4uY29tL3ZpZGVvLnBocD9pZD0zNDY5ODU= Destination Link

            string[] parts = Url.Split('/');
            return parts[parts.Length-1].ToString();

        }

        /// <summary>
        /// Hash the User's Password
        /// </summary>
        /// <param name="password">
        /// string: The Password to hash
        /// </param>
        /// <returns>
        /// string: SHA512 Hash of the password
        /// </returns>
        internal string HashPassword(string password)
        {
            return hh.GetSHA512(password);
        }
        
        internal string BytesToString(byte[] reportedImage)
        {
            return Encoding.UTF8.GetString(reportedImage, 0, reportedImage.Length);
        }

        internal byte[] StringToBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
