using BonSite.Core.Domain.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Services
{
    public class Log
    {

        public static bool Add(LogInfo logInfo)
        {

            return BonSite.Data.Log.Add(logInfo);
        }

        public static DataTable QueryLog(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Log.QueryLog(pageSize, pageNumber, condition, sort);
        }
        public static int Count()
        {
            return BonSite.Data.Log.Count();
        }
    }
}
