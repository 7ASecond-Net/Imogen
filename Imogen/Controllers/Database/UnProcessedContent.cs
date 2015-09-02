using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Imogen.Model;

namespace Imogen.Controllers.Database
{
    class UnProcessedContent
    {

        public EUReported GetUnProcessedContent()
        {
           return DBHelper.GetNextUnprocessedRecord();
        }
    }
}
