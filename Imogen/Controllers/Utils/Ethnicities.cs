using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Utils
{
    class Ethnicities
    {

        /* - UK Codes
        IC1	White - North European
        IC2	White - South European
        IC3	Black
        IC4	Asian (in the UK Asian refers to people from the Indian subcontinent like India, Pakistan, Bangladesh, Nepal)
        IC5	Chinese, Japanese, or other South East Asian
        IC6	Arabic or North African
        IC9	Unknown
        */

        /* http://www.ir.ufl.edu/OIRApps/ethnic_code_changes/info.html #New Race and Ethnicity Standards
        USA There are now five categories for data on race:
        1) American Indian or Alaska Native
        2) Asian
        3) Black or African American
        4) Native Hawaiian or Other Pacific Islander
        5) White
        */

        // Hard coded for just now
        //TODO: Get Users Jurisdiction and Load Ethnic Codes for that Jurisdiction
        internal List<string> GetAvailableEthnicCodes()
        {
            List<string> tmpList = new List<string>();

            tmpList.Add("IC1	White - North European");
            tmpList.Add("IC2	White - South European");
            tmpList.Add("IC3	Black");
            tmpList.Add("IC4	Asian");
            tmpList.Add("IC5	Chinese, Japanese, or other South East Asian");
            tmpList.Add("IC6	Arabic or North African");
            tmpList.Add("IC9	Unknown");

            return tmpList;
        }
    }
}
