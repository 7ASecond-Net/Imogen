using Imogen.Controllers.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Reporting
{
    public static class CurrentInternalReport
    {


        public static Dictionary<int, string> ARCXRatingDescription = new Dictionary<int, string>();
        public enum ARCXRating
        {
            A,
            R,
            C,
            X,
            N
        }

        public static void Init()
        {
            ARCXRatingDescription.Clear();
            ARCXRatingDescription.Add((int)ARCXRating.A, "Unlimited Access - Anyone can view this content");
            ARCXRatingDescription.Add((int)ARCXRating.R, "Restricted Access - some people are not allowed access to this content");
            ARCXRatingDescription.Add((int)ARCXRating.C, "No Access - Criminal Content");
            ARCXRatingDescription.Add((int)ARCXRating.X, "No longer Accessible - The content may have been removed");
            ARCXRatingDescription.Add((int)ARCXRating.N, "Access has not been determined yet");

            SrcUrlARCXRating = ARCXRating.N;
            LinkUrlARCXRating = ARCXRating.N;

            SrcSaved = false;
        }

        public static int ReportNumber { get; set; }
        public static DateTime ReportedOn { get; set; }
        public static DateTime UpdatedOn { get; set; }
        public static int ReportedBy { get; set; }

        public static string PageUrl { get; set; }
        public static string PageUrlHash { get; set; }
        public static string SrcUrl { get; set; }
        public static string SrcUrlHash { get; set; }
        public static string LinkUrl { get; set; }
        public static string LinkUrlHash { get; set; }
        public static string TrueLinkUrl { get; set; }
        public static string TrueLinkUrlHash { get; set; }

        public static string PageUrlFilename { get; set; }
        public static string SrcUrlFilename { get; set; }
        public static string LinkUrlFilename { get; set; }
        public static string TrueLinkUrlFilename { get; set; }

        public static ARCXRating SrcUrlARCXRating { get; set; }
        public static ARCXRating LinkUrlARCXRating { get; set; }

        public static string ImagePath { get; set; }
        public static bool Completed { get; set; }
        public static double ReportSessionTime { get; internal set; }

        public static bool SrcSaved { get; set; }

        /// <summary>
        /// Save the Contents of this class to Damocles
        /// IF
        /// The User Is Logging Off and this case has not been processed yet
        /// </summary>
        internal static void Save()
        {
            DBHelper.SaveCurrentReport();
        }

        /// <summary>
        /// Attempt to Load the Current Case if it Exists From Damocles
        /// </summary>
        internal static void Load()
        {
            Reset();
            DBHelper.LoadCurrentReport();
        }

        internal static void Reset()
        {
            ReportNumber = -1;
            ReportedBy = -1;
            PageUrl = null;
            PageUrlHash = null;
            PageUrlFilename = null;
            SrcUrl = null;
            SrcUrlARCXRating = ARCXRating.N;
            SrcUrlFilename = null;
            SrcUrlHash = null;
            LinkUrl = null;
            LinkUrlARCXRating = ARCXRating.N;
            LinkUrlFilename = null;
            LinkUrlHash = null;
            TrueLinkUrl = null;
            TrueLinkUrlFilename = null;
            TrueLinkUrlHash = null;
            ImagePath = null;

            Init();
        }
    }


    /// <summary>
    /// The Current User is not Saved - it is recreated on every Login.
    /// </summary>
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string UserPassword { get; set; }
        public static string UserDisplayName { get; set; }
        public static string UserJurisdiction { get; set; }
        public static string UserRank { get; set; }
        public static double SessionSecondsTotal { get; set; }
        public static double ThisSessionSeconds { get; set; }
        public static bool RememberMe { get; set; }
    }

    public static class GlobalSettings
    {
        public const string RestrictedBrowserUserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"; //TODO: Shouldn't be Hard Coded
        public const string FileSavePath = @"C:\Users\Dave\Pictures\Imogen"; //TODO: Shouldn't be Hard Coded
    }




}
