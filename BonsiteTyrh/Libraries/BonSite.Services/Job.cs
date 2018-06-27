using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Services
{
    public class Job
    {


        #region 增删改查
        public static int Create(JobInfo model)
        {
            return BonSite.Data.Job.Create(model);
        }

        public static JobInfo GetModelByJobID(int jobid)
        {
            if (jobid > 0)
                return BonSite.Data.Job.GetModelById(jobid);
            return null;
        }

        public static bool Update(JobInfo model)
        {
            return BonSite.Data.Job.Update(model);
        }

        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.Job.Delete(CommonHelper.IntArrayToString(idlist));
                //删除缓存
                //BonSite.Core.BSCache.Remove(CacheKeys.SITE_ARTICLECLASS_LIST + "\\d+");
                return isSucces;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 后台

        public static DataTable AdminGetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Job.AdminGetJobList(pageSize, pageNumber, condition, sort);
        }

        public static int AdminGetJobCount(string condition)
        {
            return BonSite.Data.Job.AdminGetJobCount(condition);
        }

        public static string AdminGetJobListCondition(string title)
        {
            return BonSite.Data.Job.AdminGetJobListCondition(title);
        }

        public static string AdminGetJobListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Job.AdminGetJobListSort(sortColumn, sortDirection);
        }

        #endregion


        public static DataTable GetJobList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Job.GetJobList(pageSize, pageNumber, condition, sort);
        }

        public static int GetJobCount(string condition)
        {
            return BonSite.Data.Job.GetJobCount(condition);
        }

        public static string GetJobListCondition(string city, string jobtitle)
        {
            return BonSite.Data.Job.GetJobListCondition(city, jobtitle);
        }

        public static string GetJobListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Job.GetJobListSort(sortColumn, sortDirection);
        }

        public static DataTable GetJobCityList()
        {
            return BonSite.Data.Job.GetJobCityList();
        }
    }
}
