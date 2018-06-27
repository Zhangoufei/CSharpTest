using BonSite.Core.Domain.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public partial interface IRDBSStrategy
    {
        bool Add(LogInfo log);

        bool delete(int id);

        bool update(LogInfo log);

        DataTable QueryLog(int pageSize, int pageNumber, string condition, string sort);

        int Count();
    }
}
