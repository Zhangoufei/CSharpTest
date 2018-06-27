using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy:IRDBSStrategy
    {

        public int CreateJob(JobInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@JobTitle",  SqlDbType.NVarChar, 50,model.JobTitle),                         
                         GenerateInParam("@PubDate",  SqlDbType.DateTime,8, model.PubDate),                       
                         GenerateInParam("@EndDate",  SqlDbType.DateTime,8, model.EndDate),      
                         GenerateInParam("@Number",  SqlDbType.Int,4, model.Number),      
                         GenerateInParam("@State",SqlDbType.Int,4,model.State),
                         GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                         
                         GenerateInParam("@City",  SqlDbType.NVarChar,50, model.City)
              
            };

            string commandText = string.Format("insert into {0}Jobs (JobTitle,PubDate,EndDate,Number,State,Body,City) values (@JobTitle,@PubDate,@EndDate,@Number,@State,@Body,@City) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        public bool DeleteJob(string jobIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}Jobs] WHERE [JobID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                jobIdList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        public bool UpdateJob(JobInfo model)
        {
            DbParameter[] parms =
            {
                GenerateInParam("@JobTitle", SqlDbType.NVarChar, 50, model.JobTitle),
                GenerateInParam("@PubDate", SqlDbType.DateTime, 8, model.PubDate),
                GenerateInParam("@EndDate", SqlDbType.DateTime, 8, model.EndDate),
                GenerateInParam("@Number", SqlDbType.Int, 4, model.Number),
                GenerateInParam("@State", SqlDbType.Int, 4, model.State),
                GenerateInParam("@Body", SqlDbType.NText, 0, model.Body),
                GenerateInParam("@City", SqlDbType.NVarChar, 50, model.City),
                GenerateInParam("@JobID", SqlDbType.Int, 4, model.JobID)

            };

            string commandText = string.Format("update {0}Jobs set JobTitle=@JobTitle,PubDate=@PubDate,EndDate=@EndDate,Number=@Number,State=@State,Body=@Body,City=@City where JobID=@JobID ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        public System.Data.IDataReader GetJobById(int jobId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@jobId", SqlDbType.Int, 4, jobId)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}jobs] WHERE [jobId]=@jobId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.JOBS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public System.Data.DataTable GetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}jobs] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}jobs] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}jobs]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}jobs] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string GetJobListCondition(string city, string jobTitle)
        {
            StringBuilder condition = new StringBuilder();

            if (!city.Equals("全部"))
                condition.AppendFormat(" AND [city] = '{0}' ", city);
            if (!string.IsNullOrWhiteSpace(jobTitle))
                condition.AppendFormat(" AND [jobTitle] like '{0}%' ", jobTitle);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string GetJobListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[JobID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public int GetJobCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(JobID) FROM [{0}jobs]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(JobID) FROM [{0}jobs] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public System.Data.DataTable AdminGetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}jobs] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}jobs] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}jobs]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}jobs] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.JOBS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string AdminGetJobListCondition(string title)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [jobTitle] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string AdminGetJobListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[JobID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }


        public int AdminGetJobCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(JobID) FROM [{0}jobs]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(JobID) FROM [{0}jobs] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }



        public DataTable GetJobCityList()
        {
            string commandText = string.Format("SELECT DISTINCT City FROM {0}Jobs", RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

    }
}
