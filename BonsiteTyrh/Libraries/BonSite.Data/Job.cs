using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Data
{
    public class Job
    {

        #region 辅助方法
        /// <summary>
        /// 通过IDataReader创建ShopInfo
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static JobInfo BuildFromReader(IDataReader reader)
        {
            JobInfo model=new JobInfo();

            model.JobID = TypeHelper.ObjectToInt(reader["JobID"]);
            model.JobTitle = reader["JobTitle"].ToString();
            model.PubDate = TypeHelper.ObjectToDateTime(reader["PubDate"]);
            model.EndDate = TypeHelper.ObjectToDateTime(reader["EndDate"]);
            model.Number = TypeHelper.ObjectToInt(reader["Number"]);
            model.State = TypeHelper.ObjectToInt(reader["State"]);
            model.Body = reader["Body"].ToString();
            model.City = reader["City"].ToString();

            return model;
        }
        #endregion

        public static int Create(JobInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateJob(model);
        }

        public static bool Delete(string jobidlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteJob(jobidlist);
        }

        public static bool Update(JobInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateJob(model);
        }

        public static JobInfo GetModelById(int JobId)
        {
            JobInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetJobById(JobId);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();

            return model;
        }

        public static DataTable AdminGetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetJobList(pageSize, pageNumber, condition, sort);
        }

        public static string AdminGetJobListCondition(string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetJobListCondition(title);

        }

        public static string AdminGetJobListSort(string sortcolumn, string sortdirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetJobListSort(sortcolumn, sortdirection);
        }

        public static int AdminGetJobCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetJobCount(condition);
        }

        public static DataTable GetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.GetJobList(pageSize, pageNumber, condition, sort);
        }

        public static string GetJobListCondition(string city,string jobtitle)
        {
            return BonSite.Core.BSData.RDBS.GetJobListCondition(city, jobtitle);

        }

        public static string GetJobListSort(string sortcolumn, string sortdirection)
        {
            return BonSite.Core.BSData.RDBS.GetJobListSort(sortcolumn, sortdirection);
        }

        public static int GetJobCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.GetJobCount(condition);
        }

        public static DataTable GetJobCityList()
        {
            return BonSite.Core.BSData.RDBS.GetJobCityList();
        }
    }
}
