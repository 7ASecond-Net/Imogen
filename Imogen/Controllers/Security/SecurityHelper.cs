using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Security
{
    class SecurityHelper
    {

        /// <summary>
        /// Gets the name of the current logged on user
        /// </summary>
        /// <returns>
        /// string: The logged on User according to the system - includes domain or computer name.
        /// </returns>
        internal string GetUser()
        {
            string machineName = Environment.MachineName;
            string username = Environment.UserName;
            return machineName + "/" + username;
        }
    }
}
