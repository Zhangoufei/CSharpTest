using BonSite.Core.Domain.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Data
{
    public class Log
    {

        public static bool Add(LogInfo log)
        {
            return BonSite.Core.BSData.RDBS.Add(log);
        }


        public static DataTable QueryLog(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.QueryLog(pageSize, pageNumber, condition, sort);
        }

        public static int Count() {

            return BonSite.Core.BSData.RDBS.Count();
        }
    }
}
