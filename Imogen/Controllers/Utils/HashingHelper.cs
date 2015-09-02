using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Utils
{
    class HashingHelper
    {
        /// <summary>
        /// Produce a SHA512 Hash of a string
        /// </summary>
        /// <param name="stringData">
        /// string: The string to be hashed
        /// </param>
        /// <returns>
        /// string: A Hexadecimal string hash
        /// </returns>
        public string GetSHA512(string stringData)
        {
            if (!string.IsNullOrEmpty(stringData))
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] message = UE.GetBytes(stringData);
                SHA512Managed hashString = new SHA512Managed();
                string hexNumber = "";
                byte[] hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hexNumber += String.Format("{0:x2}", x);
                }
                return hexNumber;
            }

            return string.Empty;
        }
        
        internal string GetMD5(string idata)
        {
            if (!string.IsNullOrEmpty(idata))
            {
                // step 1, calculate MD5 hash from input
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(idata);
                byte[] hash = md5.ComputeHash(inputBytes);

                // step 2, convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        internal string GetSHA1(string idata)
        {
            if (!string.IsNullOrEmpty(idata))
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] message = UE.GetBytes(idata);
                SHA1Managed hashString = new SHA1Managed();
                string hexNumber = "";
                byte[] hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hexNumber += String.Format("{0:x2}", x);
                }
                return hexNumber;
            }

            return string.Empty;
        }

        internal string GetSHA256(string idata)
        {
            if (!string.IsNullOrEmpty(idata))
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] message = UE.GetBytes(idata);
                SHA256Managed hashString = new SHA256Managed();
                string hexNumber = "";
                byte[] hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hexNumber += String.Format("{0:x2}", x);
                }
                return hexNumber;
            }

            return string.Empty;
        }

        internal string GetSHA384(string idata)
        {
            if (!string.IsNullOrEmpty(idata))
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] message = UE.GetBytes(idata);
                SHA384Managed hashString = new SHA384Managed();
                string hexNumber = "";
                byte[] hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hexNumber += String.Format("{0:x2}", x);
                }
                return hexNumber;
            }

            return string.Empty;
        }

        internal string GetssDeep(string idata)
        {

            return string.Empty;
        }
    }
}
