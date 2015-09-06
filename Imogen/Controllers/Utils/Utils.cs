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
        internal string GetNewSrcFilename(string ext = "jpg")
        {
            string[] parts = Properties.Settings.Default.ProfilePossibleFileName1.Split('.');
            if (parts.Count() == 2) // try and get the real extension
                ext = Properties.Settings.Default.ProfilePossibleFileName1.Split('.')[1];
            return Path.Combine(Properties.Settings.Default.FileSavePath, Path.ChangeExtension(Guid.NewGuid().ToString().Replace("-", "").ToLowerInvariant(), ext));
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
            return parts[parts.Length - 1].ToString();

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

        // Source: http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        public string HowLongAgo(DateTime yourDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
