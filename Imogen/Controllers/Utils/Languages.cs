using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Imogen.Controllers.Utils
{
    class Languages
    {

        public List<string> GetAvailableLanguages()
        {
            List<string> allLanguages = new List<string>();
            CultureInfo[] cis = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach(CultureInfo ci in cis)
            {
                allLanguages.Add(ci.EnglishName);
            }

            return allLanguages;
        }

    }
}
