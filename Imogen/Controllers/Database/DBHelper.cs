using Imogen.Controllers.Utils;
using Imogen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Imogen.Controllers.Database
{
    class DBHelper
    {
        public bool DBConnected { get; internal set; }
        private Damocles2Entities de = new Damocles2Entities();

        private Utils.Utils utils = new Utils.Utils();
        private HashingHelper hh = new HashingHelper();
        public static double TotalSessions;
        internal static string UserRank;
        internal static string UJurisdiction;

        private static string userName { get; set; }
        private static string userPasswordHash { get; set; }

        public DBHelper()
        {
            ConnectToDamocles();
            de.Database.Connection.StateChange += Connection_StateChange;
            SetConnectionStatus();
        }





        #region Connections

        private void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            SetConnectionStatus();
        }

        internal static string GetImagesReported()
        {
            Damocles2Entities de = new Damocles2Entities();
            HashingHelper hh = new HashingHelper();
            string passwordHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User u = de.Users.Where(usr => usr.Username == Properties.Settings.Default.UserUsername && usr.UserPassword == passwordHash).FirstOrDefault();
            var pr = de.ProcessingResults.Where(pi => pi.id == u.Id);

            int iCount = 0;
            foreach (ProcessingResult p in pr)
            {
                if (p.CSrcResultId != null) iCount++;
            }

            return iCount.ToString("N0");
        }

        internal static string GetVideosReported()
        {
            Damocles2Entities de = new Damocles2Entities();
            HashingHelper hh = new HashingHelper();
            string passwordHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User u = de.Users.Where(usr => usr.Username == Properties.Settings.Default.UserUsername && usr.UserPassword == passwordHash).FirstOrDefault();
            var pr = de.ProcessingResults.Where(pi => pi.id == u.Id);

            int iCount = 0;
            foreach (ProcessingResult p in pr)
            {
                if (p.CLinkResultId != null) iCount++;
            }

            return iCount.ToString("N0");
        }

        internal static string GetVideosInvestigated()
        {
            Damocles2Entities de = new Damocles2Entities();
            HashingHelper hh = new HashingHelper();
            string passwordHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User u = de.Users.Where(usr => usr.Username == Properties.Settings.Default.UserUsername && usr.UserPassword == passwordHash).FirstOrDefault();
            var pr = de.ProcessingResults.Where(pi => pi.UserId == u.Id);

            int iCount = 0;
            foreach (ProcessingResult p in pr)
            {
                if (p.ALinkResultId != null) iCount++;
                if (p.CLinkResultId != null) iCount++;
                if (p.RLinkResultId != null) iCount++;
            }

            return iCount.ToString("N0");
        }

        internal static string GetImagesInvestigated()
        {
            Damocles2Entities de = new Damocles2Entities();
            HashingHelper hh = new HashingHelper();
            string passwordHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User u = de.Users.Where(usr => usr.Username == Properties.Settings.Default.UserUsername && usr.UserPassword == passwordHash).FirstOrDefault();
            var pr = de.ProcessingResults.Where(pi => pi.UserId == u.Id);

            int iCount = 0;
            foreach (ProcessingResult p in pr)
            {
                if (p.ASrcResultId != null) iCount++;
                if (p.CSrcResultId != null) iCount++;
                if (p.RSrcResultId != null) iCount++;
            }

            return iCount.ToString("N0");
        }

        private void SetConnectionStatus()
        {
            switch (de.Database.Connection.State)
            {
                case System.Data.ConnectionState.Closed:
                    DBConnected = false;
                    ConnectToDamocles();
                    break;
                case System.Data.ConnectionState.Open:
                    DBConnected = true;
                    break;
                case System.Data.ConnectionState.Connecting:
                    DBConnected = true;
                    break;
                case System.Data.ConnectionState.Executing:
                    DBConnected = true;
                    break;
                case System.Data.ConnectionState.Fetching:
                    DBConnected = true;
                    break;
                case System.Data.ConnectionState.Broken:
                    DBConnected = false;
                    break;
                default:
                    DBConnected = false;
                    break;
            }
        }

        //TODO: Not completed Yet
        // LinkARCRating will be in either - the GoneButNotForgotten table (files that are no longer available)
        // or ... Processing Results
        internal static string GetLinkARCValue(int reportId)
        {
            Damocles2Entities de = new Damocles2Entities();
            var gbResult = de.GoneButNotForgottenLinks.Where(gid => gid.Id == reportId).FirstOrDefault();
            // If it exists
            //TODO: Really need the error code saved in GoneButNotForgotten
            if (gbResult != null)
                return "Link Url Contents Not Available " + gbResult.LastCheckedOn;
            return string.Empty;
        }

        //TODO: Not completed Yet
        // LinkARCRating will be in either - the GoneButNotForgotten table (files that are no longer available)
        // or ... Processing Results
        internal static string GetSrcARCRating(int reportId)
        {
            Damocles2Entities de = new Damocles2Entities();
            var gbResult = de.GoneButNotForgottenLinks.Where(gid => gid.Id == reportId).FirstOrDefault();
            // If it exists
            //TODO: Really need the error code saved in GoneButNotForgotten
            if (gbResult != null)
                return "Src Url Contents Not Available " + gbResult.LastCheckedOn;
            return string.Empty;
        }

        /// <summary>
        /// Tells us if the Report was of a A R C or X type
        /// </summary>
        /// <param name="reportId">
        /// nnt: The ReportId we are looking up
        /// </param>
        /// <returns>
        /// string: A R C or X Where X = GoneButNotForgotten
        /// </returns>
        /// <remarks>
        /// The ReportId can be derived from: Convert.ToInt32(Properties.Settings.Default.ProfileReportNumber)
        /// </remarks>
        internal static string GetLinkARCRating(int reportId)
        {
            Damocles2Entities de = new Damocles2Entities();
            var gbResult = de.GoneButNotForgottenLinks.Where(gid => gid.Id == reportId).FirstOrDefault();
            // If it exists
            //TODO: Really need the error code saved in GoneButNotForgotten
            if (gbResult != null)
                return "X";
            ProcessingResult pr = de.ProcessingResults.Where(pid => pid.id == reportId).FirstOrDefault();
            if (pr.ASrcResultId != null)
                return "A";
            if (pr.RSrcResultId != null)
                return "R";
            if (pr.CSrcResultId != null)
                return "C";

            return string.Empty; // We May not have a Src - it may be the whole page that has been reported, or only a link!
        }


        internal static ProcessingResult GeProcessingResultById(int reportId)
        {
            Damocles2Entities de = new Damocles2Entities();
            var gbResult = de.ProcessingResults.Where(gid => gid.id == reportId).FirstOrDefault();
            // If it exists
            //TODO: Really need the error code saved in GoneButNotForgotten

            return gbResult;
        }

        internal static void SaveIndividualsBasicInformation(string Name, string Age, string Sex, string SpokenLanguage, string WrittenLanguage, string Nationality, string Ethnicity, Image image)
        {
            Utils.Utils utils = new Utils.Utils();
            Damocles2Entities de = new Damocles2Entities();
            FaceARC farc = new FaceARC();
            Face face = new Face();

            // Possible Return Values = X, A, R, C, String.Empty
            var pr = GeProcessingResultById(Convert.ToInt32(Properties.Settings.Default.ProfileReportNumber.Replace(",", "")));
            if (pr != null)
            {
                if (pr.CSrcResultId != null)    // Theoretically the most common result
                    farc.CId = pr.CSrcResultId;

                if (pr.RSrcResultId != null)
                    farc.RId = pr.RSrcResultId;

                if (pr.ASrcResultId != null) // Theoretically the least common result
                    farc.AId = pr.ASrcResultId;

                farc.FaceId = face.id;
                face.FaceData = utils.BytesToString(utils.imageToByteArray(image));
                face.CreatedOn = DateTime.UtcNow;
                face.UpdatedOn = DateTime.UtcNow;
                face.CreatedBy = Properties.Settings.Default.UserId;
                if (!string.IsNullOrEmpty(Name))
                    face.Name = Name;
                if (!string.IsNullOrEmpty(Age))
                    face.Age = Convert.ToInt32(Age);
                if (!string.IsNullOrEmpty(Sex))
                    face.Sex = Sex;
                if (!string.IsNullOrEmpty(SpokenLanguage))
                    face.SpokenLanguage = SpokenLanguage;
                if (!string.IsNullOrEmpty(WrittenLanguage))
                    face.WrittenLanguage = WrittenLanguage;
                if (!string.IsNullOrEmpty(Nationality))
                    face.Nationality = Nationality;
                if (!string.IsNullOrEmpty(Ethnicity))
                    face.Ethnicity = Ethnicity;

                de.Faces.Add(face);
                de.FaceARCs.Add(farc);
                de.SaveChanges();
            }
            else
            {
                // Need to save the Profile results before trying to get them from ProcessingResults.
            }
        }

        internal void SaveSha512Hash(string fSha512)
        {
            Damocles2Entities de = new Damocles2Entities();
            Hash h = new Hash();
            h.CreatedOn = DateTime.UtcNow;
            h.HashType = "SHA512";
            h.HashValue = fSha512;
            h.id = GetRecordId();
            h.UpdatedOn = DateTime.UtcNow;
            de.Hashes.Add(h);
            de.SaveChanges();
        }

        internal void SaveSha256Hash(string fSha256)
        {
            Damocles2Entities de = new Damocles2Entities();
            Hash h = new Hash();
            h.CreatedOn = DateTime.UtcNow;
            h.HashType = "SHA256";
            h.HashValue = fSha256;
            h.id = GetRecordId();
            h.UpdatedOn = DateTime.UtcNow;
            de.Hashes.Add(h);
            de.SaveChanges();
        }

        internal void SaveSha1Hash(string fSha1)
        {
            Damocles2Entities de = new Damocles2Entities();
            Hash h = new Hash();
            h.CreatedOn = DateTime.UtcNow;
            h.HashType = "SHA1";
            h.HashValue = fSha1;
            h.id = GetRecordId();
            h.UpdatedOn = DateTime.UtcNow;
            de.Hashes.Add(h);
            de.SaveChanges();
        }

        internal void SaveMD5Hash(string fMd5)
        {
            Damocles2Entities de = new Damocles2Entities();
            Hash h = new Hash();
            h.CreatedOn = DateTime.UtcNow;
            h.HashType = "MD5";
            h.HashValue = fMd5;
            h.id = GetRecordId();
            h.UpdatedOn = DateTime.UtcNow;
            de.Hashes.Add(h);
            de.SaveChanges();
        }

        private int GetRecordId()
        {
            return Convert.ToInt32(Properties.Settings.Default.ProfileReportNumber.Replace(",", ""));
        }

        private void ConnectToDamocles()
        {
            de.Database.Connection.Open();
        }

        internal void Connect()
        {
            ConnectToDamocles();
        }

        #endregion

        #region 
        //TODO: This should be retrieved from a Queue via API or Messaging.
        //TODO: Convert the EUReported class to an internal class called - CurrentReport.
        internal static EUReported GetNextUnprocessedRecord()
        {
            Damocles2Entities de = new Damocles2Entities();
            EUReported eur = de.EUReporteds.Where(r => r.Processed == false).FirstOrDefault();
            return eur;

        }
        #endregion

        #region Users
        internal bool Login(string username, string password)
        {
            string passwordHash = utils.HashPassword(password);
            userName = username;
            userPasswordHash = passwordHash;
            Damocles2Entities de = new Damocles2Entities();
            User user = de.Users.Where(u => u.Username == username && u.UserPassword == passwordHash).FirstOrDefault();
            if (user == null)
                return false;

            Properties.Settings.Default.UserId = user.Id;

            //TODO: This is not functioning correctly - simply want the User's current rank.
            UserRank ur = de.UserRanks.Where(usr => usr.UserId == user.Id).FirstOrDefault();
            user.IsOnline = true;
            UserRank = ur.Rank.RankNameEnglish;

            UserJurisdiction uj = de.UserJurisdictions.Where(usrj => usrj.UserId == user.Id).FirstOrDefault();
            if (uj != null)
            {
                if (uj.Jurisidction.Country == uj.Jurisidction.State)
                    UJurisdiction = uj.Jurisidction.Country;
                else
                    UJurisdiction = uj.Jurisidction.State.Trim() + " in " + uj.Jurisidction.Country.Trim();
            }
            else
                UJurisdiction = "Unknown";


            var aus = de.UsersSessions.Where(auss => auss.id == user.Id);

            foreach (UsersSession userS in aus)
            {
                Properties.Settings.Default.SessionSecondsTotal += userS.SessionSeconds;
            }

            UsersSession us = new UsersSession();
            us.LoggedOnAt = DateTime.UtcNow;
            us.id = user.Id;
            de.UsersSessions.Add(us);

            de.SaveChanges();
            return true;
        }

        internal bool RegisterUser(string username, string password, string email, string displayName)
        {
            try
            {
                string passwordHash = utils.HashPassword(password);
                Damocles2Entities de = new Damocles2Entities();
                User u = new User();
                u.CreatedOn = DateTime.UtcNow;
                u.UpdatedOn = DateTime.UtcNow;
                u.Username = username;
                u.UserPassword = passwordHash;
                u.EmailAddress = email;
                u.UserDisplayName = displayName;
                u.IsOnline = false; // Just registered Not logged in yet!
                de.Users.Add(u);
                de.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO: Better logging than this is required.
                return false;
            }
        }

        internal void LogOff()
        {
            Damocles2Entities de = new Damocles2Entities();
            string passwordHash = utils.HashPassword(Properties.Settings.Default.UserPassword);
            User u = de.Users.Where(usr => usr.Username == Properties.Settings.Default.UserUsername && usr.UserPassword == passwordHash).FirstOrDefault();
            // UsersSession us = de.UsersSessions.Where(uss => uss.id == u.Id && uss.loggedOffAt == null)
            // v1.OrderByDescending(rec => rec.Id).FirstOrDefault();
            UsersSession us = de.UsersSessions.Where(uss => uss.id == u.Id && uss.loggedOffAt == null).OrderByDescending(ob => ob.LoggedOnAt).FirstOrDefault();
            us.loggedOffAt = DateTime.UtcNow;
            us.SessionSeconds = Properties.Settings.Default.SessionSeconds;
            u.IsOnline = false;
            de.SaveChanges();
        }

        internal double GetUserLifetimeSessionTime()
        {
            Damocles2Entities de = new Damocles2Entities();
            if (de.Database.Connection.State == System.Data.ConnectionState.Closed)
                de.Database.Connection.Open();

            User user = de.Users.Where(u => u.Username == userName && u.UserPassword == userPasswordHash).FirstOrDefault();
            if (user == null)
                return 0;


            var aus = de.UsersSessions.Where(auss => auss.id == user.Id);
            double t = 0;
            foreach (UsersSession userS in aus)
            {
                TotalSessions++;
                Properties.Settings.Default.SessionSecondsTotal += userS.SessionSeconds;
                t += userS.SessionSeconds;
            }

            return t;
        }

        #endregion




        //TODO: Refactor this
        internal void SetSrcToAllowed(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.SrcUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            // Only add records if they have not already been created (for just now)
            if (pr.ASrcResultId == null)
            {
                A aRecord = new A();
                aRecord.ResultCount = aRecord.ResultCount + 1;
                aRecord.UpdatedOn = DateTime.UtcNow;
                aRecord.CreatedOn = DateTime.UtcNow;
                aRecord.IsAllowed = true;
                de.A.Add(aRecord);
                pr.ASrcResultId = aRecord.pid;
            }

            //if (pr.RSrcResultId == null)
            //{
            //    R rRecord = new R();
            //    rRecord.ResultCount = rRecord.ResultCount + 1;
            //    rRecord.UpdatedOn = DateTime.UtcNow;
            //    rRecord.CreatedOn = DateTime.UtcNow;
            //    rRecord.IsRestricted = false;
            //    de.R.Add(rRecord);
            //    pr.RSrcResultId = rRecord.pid;
            //}

            //if (pr.CSrcResultId == null)
            //{
            //    C cRecord = new C();
            //    cRecord.ResultCount = cRecord.ResultCount + 1;
            //    cRecord.UpdatedOn = DateTime.UtcNow;
            //    cRecord.CreatedOn = DateTime.UtcNow;
            //    cRecord.IsCriminal = false;
            //    de.C.Add(cRecord);
            //    pr.CSrcResultId = cRecord.pid;
            //}

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.LinkUrlHash == eu.LinkUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ALinkResultId != null || pr.RLinkResultId != null || pr.CLinkResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();
        }

        //TODO: Who is it restricted to?
        internal void SetSrcToRestricted(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.SrcUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            // Only add records if they have not already been created (for just now)
            //if (pr.ASrcResultId == null)
            //{
            //    A aRecord = new A();
            //    aRecord.ResultCount = aRecord.ResultCount + 1;
            //    aRecord.UpdatedOn = DateTime.UtcNow;
            //    aRecord.CreatedOn = DateTime.UtcNow;
            //    aRecord.IsAllowed = true;
            //    de.A.Add(aRecord);
            //    pr.ASrcResultId = aRecord.pid;
            //}

            if (pr.RSrcResultId == null)
            {
                R rRecord = new R();
                rRecord.ResultCount = rRecord.ResultCount + 1;
                rRecord.UpdatedOn = DateTime.UtcNow;
                rRecord.CreatedOn = DateTime.UtcNow;
                rRecord.IsRestricted = true;
                de.R.Add(rRecord);
                pr.RSrcResultId = rRecord.pid;
            }

            //if (pr.CSrcResultId == null)
            //{
            //    C cRecord = new C();
            //    cRecord.ResultCount = cRecord.ResultCount + 1;
            //    cRecord.UpdatedOn = DateTime.UtcNow;
            //    cRecord.CreatedOn = DateTime.UtcNow;
            //    cRecord.IsCriminal = false;
            //    de.C.Add(cRecord);
            //    pr.CSrcResultId = cRecord.pid;
            //}

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.LinkUrlHash == eu.LinkUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ALinkResultId != null || pr.RLinkResultId != null || pr.CLinkResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();

            //TODO: Implement this
            // if eu.Processed == true then raise Event to tell frmMain to go to the next unprocessed Report

        }

        internal void SetSrcToCriminal(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.SrcUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            // Only add records if they have not already been created (for just now)
            //if (pr.ASrcResultId == null)
            //{
            //    A aRecord = new A();
            //    aRecord.ResultCount = aRecord.ResultCount + 1;
            //    aRecord.UpdatedOn = DateTime.UtcNow;
            //    aRecord.CreatedOn = DateTime.UtcNow;
            //    aRecord.IsAllowed = true;
            //    de.A.Add(aRecord);
            //    pr.ASrcResultId = aRecord.pid;
            //}

            //if (pr.RSrcResultId == null)
            //{
            //    R rRecord = new R();
            //    rRecord.ResultCount = rRecord.ResultCount + 1;
            //    rRecord.UpdatedOn = DateTime.UtcNow;
            //    rRecord.CreatedOn = DateTime.UtcNow;
            //    rRecord.IsRestricted = true;
            //    de.R.Add(rRecord);
            //    pr.RSrcResultId = rRecord.pid;
            //}

            if (pr.CSrcResultId == null)
            {
                C cRecord = new C();
                cRecord.ResultCount = cRecord.ResultCount + 1;
                cRecord.UpdatedOn = DateTime.UtcNow;
                cRecord.CreatedOn = DateTime.UtcNow;
                cRecord.IsCriminal = true;
                de.C.Add(cRecord);
                pr.CSrcResultId = cRecord.pid;
            }

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.LinkUrlHash == eu.LinkUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ALinkResultId != null || pr.RLinkResultId != null || pr.CLinkResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();

            //TODO: Implement this
            // if eu.Processed == true then raise Event to tell frmMain to go to the next unprocessed Report

        }

        //TODO: Who is it restricted to?
        internal void SetLinkToRestricted(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.LinkUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            // Only add records if they have not already been created (for just now)
            //if (pr.ALinkResultId == null)
            //{
            //    A aRecord = new A();
            //    aRecord.ResultCount = aRecord.ResultCount + 1;
            //    aRecord.UpdatedOn = DateTime.UtcNow;
            //    aRecord.CreatedOn = DateTime.UtcNow;
            //    aRecord.IsAllowed = true;
            //    de.A.Add(aRecord);
            //    pr.ALinkResultId = aRecord.pid;
            //}

            if (pr.RLinkResultId == null)
            {
                R rRecord = new R();
                rRecord.ResultCount = rRecord.ResultCount + 1;
                rRecord.UpdatedOn = DateTime.UtcNow;
                rRecord.CreatedOn = DateTime.UtcNow;
                rRecord.IsRestricted = true;
                de.R.Add(rRecord);
                pr.RLinkResultId = rRecord.pid;
            }

            //if (pr.CLinkResultId == null)
            //{
            //    C cRecord = new C();
            //    cRecord.ResultCount = cRecord.ResultCount + 1;
            //    cRecord.UpdatedOn = DateTime.UtcNow;
            //    cRecord.CreatedOn = DateTime.UtcNow;
            //    cRecord.IsCriminal = true;
            //    de.C.Add(cRecord);
            //    pr.CLinkResultId = cRecord.pid;
            //}

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.SrcUrlHash == eu.SrcUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ASrcResultId != null || pr.RSrcResultId != null || pr.CSrcResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();

            //TODO: Implement this
            // if eu.Processed == true then raise Event to tell frmMain to go to the next unprocessed Report

        }

        internal void SetLinkToCriminal(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.LinkUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            // Only add records if they have not already been created (for just now)
            //if (pr.ALinkResultId == null)
            //{
            //    A aRecord = new A();
            //    aRecord.ResultCount = aRecord.ResultCount + 1;
            //    aRecord.UpdatedOn = DateTime.UtcNow;
            //    aRecord.CreatedOn = DateTime.UtcNow;
            //    aRecord.IsAllowed = true;
            //    de.A.Add(aRecord);
            //    pr.ALinkResultId = aRecord.pid;
            //}

            //if (pr.RLinkResultId == null)
            //{
            //    R rRecord = new R();
            //    rRecord.ResultCount = rRecord.ResultCount + 1;
            //    rRecord.UpdatedOn = DateTime.UtcNow;
            //    rRecord.CreatedOn = DateTime.UtcNow;
            //    rRecord.IsRestricted = true;
            //    de.R.Add(rRecord);
            //    pr.RLinkResultId = rRecord.pid;
            //}

            if (pr.CLinkResultId == null)
            {
                C cRecord = new C();
                cRecord.ResultCount = cRecord.ResultCount + 1;
                cRecord.UpdatedOn = DateTime.UtcNow;
                cRecord.CreatedOn = DateTime.UtcNow;
                cRecord.IsCriminal = true;
                de.C.Add(cRecord);
                pr.CLinkResultId = cRecord.pid;
            }

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.SrcUrlHash == eu.SrcUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ASrcResultId != null || pr.RSrcResultId != null || pr.CSrcResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();

            //TODO: Implement this
            // if eu.Processed == true then raise Event to tell frmMain to go to the next unprocessed Report

        }

        internal void SetLinkToAllowed(string url)
        {
            Damocles2Entities de = new Damocles2Entities();
            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.LinkUrl == url).FirstOrDefault();


            // Create ProcessingResult table if it does not already exist!
            var pr = de.ProcessingResults.Where(prr => prr.id == eu.id).FirstOrDefault();

            bool Update = true;
            if (pr == null)
            {
                Update = false;
                pr = new ProcessingResult();
            }

            pr.id = eu.id;
            var uju = de.UserJurisdictions.Where(uj => uj.UserId == usr.Id).FirstOrDefault();
            pr.JurisdictionId = uju.JurisdictionID;
            pr.CreatedOn = DateTime.UtcNow;
            pr.UpdatedOn = DateTime.UtcNow;
            pr.UserId = usr.Id;

            //  Only add records if they have not already been created(for just now)
            if (pr.ALinkResultId == null)
            {
                A aRecord = new A();
                aRecord.ResultCount = aRecord.ResultCount + 1;
                aRecord.UpdatedOn = DateTime.UtcNow;
                aRecord.CreatedOn = DateTime.UtcNow;
                aRecord.IsAllowed = true;
                de.A.Add(aRecord);
                pr.ALinkResultId = aRecord.pid;
            }

            //if (pr.RLinkResultId == null)
            //{
            //    R rRecord = new R();
            //    rRecord.ResultCount = rRecord.ResultCount + 1;
            //    rRecord.UpdatedOn = DateTime.UtcNow;
            //    rRecord.CreatedOn = DateTime.UtcNow;
            //    rRecord.IsRestricted = true;
            //    de.R.Add(rRecord);
            //    pr.RLinkResultId = rRecord.pid;
            //}

            //if (pr.CLinkResultId == null)
            //{
            //    C cRecord = new C();
            //    cRecord.ResultCount = cRecord.ResultCount + 1;
            //    cRecord.UpdatedOn = DateTime.UtcNow;
            //    cRecord.CreatedOn = DateTime.UtcNow;
            //    cRecord.IsCriminal = true;
            //    de.C.Add(cRecord);
            //    pr.CLinkResultId = cRecord.pid;
            //}

            if (!Update)
                de.ProcessingResults.Add(pr);

            // Check to see if the LinkUrl is GoneButNotForgotten
            var gbnf = de.GoneButNotForgottenLinks.Where(gbn => gbn.SrcUrlHash == eu.SrcUrlHash).FirstOrDefault();
            if (gbnf != null)
                eu.Processed = true;
            else
            {
                // this record had already been created so we may have to mark EU as processed - check now
                if (pr.ASrcResultId != null || pr.RSrcResultId != null || pr.CSrcResultId != null)
                    eu.Processed = true;
            }

            eu.UpdatedOn = DateTime.UtcNow;
            de.SaveChanges();

            //TODO: Implement this
            // if eu.Processed == true then raise Event to tell frmMain to go to the next unprocessed Report

        }

        // This is a 404 or some other error - do a check for a month to see if it returns
        // This can be done within Imogen as Background House keeping
        internal void SetLinkToGoneButNotForgotten(string url)
        {
            Damocles2Entities de = new Damocles2Entities();

            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.LinkUrl == url).FirstOrDefault();
            eu.UpdatedOn = DateTime.UtcNow;

            GoneButNotForgottenLink gb = new GoneButNotForgottenLink();
            gb.CreatedOn = DateTime.UtcNow;
            gb.Id = eu.id;
            gb.LastCheckedOn = DateTime.UtcNow;
            gb.LinkUrlHash = eu.LinkUrlHash;
            gb.ReportedBy = usr.Id;
            de.GoneButNotForgottenLinks.Add(gb);
            try
            {
                de.SaveChanges();
            }
            catch (Exception)
            {

            }


            de.Dispose();
            gb = null;
            eu = null;
            usr = null;

        }

        internal void SetSrcToGoneButNotForgotten(string url)
        {
            Damocles2Entities de = new Damocles2Entities();

            string pHash = hh.GetSHA512(Properties.Settings.Default.UserPassword);
            User usr = de.Users.Where(u => u.Username == Properties.Settings.Default.UserUsername && u.UserPassword == pHash).FirstOrDefault();

            EUReported eu = de.EUReporteds.Where(l => l.SrcUrl == url).FirstOrDefault();
            eu.UpdatedOn = DateTime.UtcNow;

            GoneButNotForgottenLink gb = new GoneButNotForgottenLink();
            gb.CreatedOn = DateTime.UtcNow;
            gb.Id = eu.id;
            gb.LastCheckedOn = DateTime.UtcNow;
            gb.SrcUrlHash = eu.SrcUrlHash;
            gb.ReportedBy = usr.Id;
            de.GoneButNotForgottenLinks.Add(gb);
            de.SaveChanges();

            de.Dispose();
            gb = null;
            eu = null;
            usr = null;
        }

        #region Statistics
        internal static string GetPendingReportCount()
        {
            Damocles2Entities de = new Damocles2Entities();
            var eur = de.EUReporteds.Where(p => p.Processed == false);
            return eur.Count().ToString("N0");
        }

        internal static string GetUsersOnlineCount()
        {
            Damocles2Entities de = new Damocles2Entities();
            var ur = de.Users.Where(p => p.IsOnline == true);
            return ur.Count().ToString("N0");
        }
        #endregion

    }
}
