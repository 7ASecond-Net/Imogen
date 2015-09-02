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

        private string userName { get; set; }
        private string userPasswordHash { get; set; }

        public DBHelper()
        {
            ConnectToDamocles();
            de.Database.Connection.StateChange += Connection_StateChange;
            SetConnectionStatus();
        }

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



        #region 
        //TODO: This should be retrieved from a Queue via API or Messaging.
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

            user.IsOnline = true;

            var aus = de.UsersSessions.Where(auss => auss.id == user.Id);
            
            foreach(UsersSession userS in aus)
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

        #endregion


        private void ConnectToDamocles()
        {
            de.Database.Connection.Open();
        }

        internal void Connect()
        {
            ConnectToDamocles();
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
                Properties.Settings.Default.SessionSecondsTotal += userS.SessionSeconds;
                t += userS.SessionSeconds;
            }

            return t;
        }
    }
}
