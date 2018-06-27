using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Data
{
    public class ServiceEval
    {

        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ProductFeedbacksInfo
        /// </summary>
        public static ServiceEvalInfo BuildFromReader(IDataReader reader)
        {
            ServiceEvalInfo model = new ServiceEvalInfo();

            model.Id = TypeHelper.ObjectToInt(reader["Id"]);
            if (reader["Name"] != null)
                model.Name = reader["Name"].ToString();
            else
                model.Name = "";
            
            if (reader["Contact"] != null)
                model.Contact = reader["Contact"].ToString();
            else
                model.Contact = "";


            if (reader["Province"] != null)
                model.Province = reader["Province"].ToString();
            else
                model.Province = "";

            if (reader["City"] != null)
                model.City = reader["City"].ToString();
            else
                model.City = "";

            if (reader["Courier"] != null)
                model.Courier = reader["Courier"].ToString();
            else
                model.Courier = "";

            model.EvalProduct = TypeHelper.ObjectToInt(reader["EvalProduct"]);
            model.EvalLogistics = TypeHelper.ObjectToInt(reader["EvalLogistics"]);
            model.EvalService = TypeHelper.ObjectToInt(reader["EvalService"]);

            model.CreateTime = TypeHelper.ObjectToDateTime(reader["CreateTime"]);

            if (reader["Body"] != null)
                model.Body = reader["Body"].ToString();
            else
                model.Body = "";
            model.State = TypeHelper.ObjectToInt(reader["State"]);

            if (reader["WeChatName"] != null)
                model.WeChatName = reader["WeChatName"].ToString();
            else
                model.WeChatName = "";

            if (reader["WeChatOpenId"] != null)
                model.WeChatOpenId = reader["WeChatOpenId"].ToString();
            else
                model.WeChatOpenId = "";

            return model;
        }

        #endregion


        /// <summary>
        /// 创建服务评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ServiceEvalInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateServiceEval(model);
        }

        /// <summary>
        /// 删除服务评价
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string idlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteServiceEvalById(idlist);
        }

        /// <summary>
        /// 更新服务评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ServiceEvalInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateServiceEval(model);
        }

        /// <summary>
        /// 获得服务评价
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static ServiceEvalInfo GetModelById(int id)
        {
            ServiceEvalInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetServiceEvalInfoById(id);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();
            return model;
        }

        public static DataTable AdminGetServiceEvalList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetServiceEvalList(pageSize, pageNumber, condition, sort);
        }

        public static string AdminGetServiceEvalListCondition(string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetServiceEvalListCondition(title);
        }

        public static string AdminGetServiceEvalListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetServiceEvalListSort(sortColumn, sortDirection);
        }

        public static int AdminGetServiceEvalCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetServiceEvalCount(condition);
        }

    }
}
