using Imogen.Controllers.Utils;
using Imogen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //TODO: NOT WORKING - user class is always null;
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

        
        internal void SetSrcToGoneButNotForgotten(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetSrcToAllowed(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetSrcToRestricted(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetSrcToCriminal(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetLinkToRestricted(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetLinkToCriminal(string url)
        {
            throw new NotImplementedException();
        }

        internal void SetLinkToAllowed(string url)
        {
            throw new NotImplementedException();
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
