using BonSite.Core;
using BonSite.Core.Domain.Log;
using BonSite.Core.Domain.Site;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy : IRDBSStrategy
    {
        public bool Add(LogInfo log)
        {
            DbParameter[] parms = {
                         GenerateInParam("@CreateTime",  SqlDbType.DateTime, 8,log.CreateTime),
                         GenerateInParam("@UserName",  SqlDbType.NVarChar,50, log.UserName),
                         GenerateInParam("@Title",  SqlDbType.NVarChar,255, log.Title),
                         GenerateInParam("@Content",  SqlDbType.NVarChar,255, log.Content),
                          GenerateInParam("@ip",  SqlDbType.NVarChar,50, log.Ip),
            };
            string commandText = string.Format("insert into {0}log (CreateTime,UserName,Title,Content,ip) values (@CreateTime,@UserName,@Title,@Content,@ip) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);

            if (TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0)
            {
                return true;
            }
            else
            {
                return false;
            };
        }

        public bool delete(int id) { return true; }

        public bool update(LogInfo log) { return true; }

        public DataTable QueryLog(int pageSize, int pageNumber, string condition, string sort)
        {

            string commandText = "";
            if (pageNumber == 1)
            {

                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}log] ORDER BY {3}",
                                              pageSize,
                                              RDBSFields.LOGSERVER,
                                              RDBSHelper.RDBSTablePre,
                                              sort);
            }
            else
            {
                commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Log]) AS [temp] WHERE [rowid] BETWEEN {4} AND {3}",
                                             RDBSFields.LOGSERVER,
                                             RDBSHelper.RDBSTablePre,
                                             sort,
                                             pageNumber * pageSize,
                                             (pageNumber - 1) * pageSize + 1);
            }
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public int Count()
        {

            string commandText = string.Format("SELECT COUNT(id) FROM [{0}log]", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }


    }
}
