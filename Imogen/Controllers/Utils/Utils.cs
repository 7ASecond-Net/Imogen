using Imogen.Controllers.Reporting;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
            string[] parts = CurrentInternalReport.SrcUrlFilename.Split('.');
            if (parts.Count() == 2) // try and get the real extension
                ext = CurrentInternalReport.SrcUrlFilename.Split('.')[1];
            return Path.Combine(GlobalSettings.FileSavePath, Path.ChangeExtension(Guid.NewGuid().ToString().Replace("-", "").ToLowerInvariant(), ext));
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

        public string HexDump(byte[] bytes, int bytesPerLine = 16)
        {
            if (bytes == null) return "<null>";
            int bytesLength = bytes.Length;

            char[] HexChars = "0123456789ABCDEF".ToCharArray();

            int firstHexColumn =
                  8                   // 8 characters for the address
                + 3;                  // 3 spaces

            int firstCharColumn = firstHexColumn
                + bytesPerLine * 3       // - 2 digit for the hexadecimal value and 1 space
                + (bytesPerLine - 1) / 8 // - 1 extra space every 8 characters from the 9th
                + 2;                  // 2 spaces 

            int lineLength = firstCharColumn
                + bytesPerLine           // - characters to show the ascii value
                + Environment.NewLine.Length; // Carriage return and line feed (should normally be 2)

            char[] line = (new String(' ', lineLength - Environment.NewLine.Length) + Environment.NewLine).ToCharArray();
            int expectedLines = (bytesLength + bytesPerLine - 1) / bytesPerLine;
            StringBuilder result = new StringBuilder(expectedLines * lineLength);

            for (int i = 0; i < bytesLength; i += bytesPerLine)
            {
                int hexColumn = firstHexColumn;
                int charColumn = firstCharColumn;

                for (int j = 0; j < bytesPerLine; j++)
                {
                    if (j > 0 && (j & 7) == 0) hexColumn++;
                    if (i + j >= bytesLength)
                    {
                        line[hexColumn] = ' ';
                        line[hexColumn + 1] = ' ';
                        line[charColumn] = ' ';
                    }
                    else
                    {
                        byte b = bytes[i + j];
                        line[hexColumn] = HexChars[(b >> 4) & 0xF];
                        line[hexColumn + 1] = HexChars[b & 0xF];
                    }
                    hexColumn += 3;
                    charColumn++;
                }
                result.Append(line);
            }
            return result.ToString().Trim();
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

        //Source: http://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
        //TODO: Need to use the correct encoder for the file extension
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            ImageCodecInfo myImageCodecInfo;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            System.Drawing.Imaging.Encoder myEncoder;
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, myImageCodecInfo, myEncoderParameters);
            return ms.ToArray();
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
