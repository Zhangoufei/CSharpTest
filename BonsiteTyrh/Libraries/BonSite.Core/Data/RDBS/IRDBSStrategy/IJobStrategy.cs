using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace BonSite.Core
{
    public partial interface IRDBSStrategy
    {
        /// <summary>
        /// 创建招聘
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        int CreateJob(JobInfo jobInfo);

        /// <summary>
        /// 删除招聘
        /// </summary>
        /// <param name="jobIdList"></param>
        /// <returns></returns>
        bool DeleteJob(string jobIdList);

        /// <summary>
        /// 更新招聘信息
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        bool UpdateJob(JobInfo jobInfo);

        /// <summary>
        /// 获取招聘信息详情
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        IDataReader GetJobById(int jobId);

        /// <summary>
        /// 前台获取招聘信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable GetJobList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 前台招聘信息搜索条件
        /// </summary>
        /// <param name="city"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        string GetJobListCondition(string city, string jobTitle);

        /// <summary>
        /// 前台招聘信息列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string GetJobListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 前台获取招聘信息数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int GetJobCount(string condition);
        
        /// <summary>
        /// 后台获取招聘信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable AdminGetJobList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获取招聘信息搜索条件
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        string AdminGetJobListCondition(string title);

        /// <summary>
        /// 后台获取招聘信息列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string AdminGetJobListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获取招聘信息数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int AdminGetJobCount(string condition);

        /// <summary>
        /// 获取招聘信息城市列表
        /// </summary>
        /// <returns></returns>
        DataTable GetJobCityList();

    }
}
