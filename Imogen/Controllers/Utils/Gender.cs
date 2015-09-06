using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Utils
{
    class Gender
    {

        //TODO: Un-Hard-Code this
        internal List<string> GetAllSexes()
        {
            List<string> tmpList = new List<string>();

            tmpList.Add("Male");
            tmpList.Add("Female");
            tmpList.Add("Transgender Male");
            tmpList.Add("Transgender Female");
            tmpList.Add("Hermaphrodite");
            tmpList.Add("Unknown");


            return tmpList;
        }
    }
}
