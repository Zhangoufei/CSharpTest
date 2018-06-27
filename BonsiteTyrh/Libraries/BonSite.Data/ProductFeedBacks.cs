using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Data
{
    public class ProductFeedBacks
    {


        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ProductFeedbacksInfo
        /// </summary>
        public static ProductFeedbacksInfo BuildFromReader(IDataReader reader)
        {
            ProductFeedbacksInfo model = new ProductFeedbacksInfo();

            model.Id = TypeHelper.ObjectToInt(reader["Id"]);
            if (reader["CityName"] != null)
                model.CityName = reader["CityName"].ToString();
            else
                model.CityName = "";

            if (reader["ProductName"] != null)
                model.ProductName = reader["ProductName"].ToString();
            else
                model.ProductName = "";

            if (reader["ProductModel"] != null)
                model.ProductModel = reader["ProductModel"].ToString();
            else
                model.ProductModel = "";

            if (reader["CustomerName"] != null)
                model.CustomerName = reader["CustomerName"].ToString();
            else
                model.CustomerName = "";

            if (reader["Contact"] != null)
                model.Contact = reader["Contact"].ToString();
            else
                model.Contact = "";

            if (reader["Address"] != null)
                model.Address = reader["Address"].ToString();
            else
                model.Address = "";

            if (reader["Body"] != null)
                model.Body = reader["Body"].ToString();
            else
                model.Body = "";

            model.State = TypeHelper.ObjectToInt(reader["State"]);

            model.CreateTime = TypeHelper.ObjectToDateTime(reader["CreateTime"]);

            if (reader["WeChatName"] != null)
                model.WeChatName = reader["WeChatName"].ToString();
            else
                model.WeChatName = "";

            if (reader["WeChatOpenId"] != null)
                model.WeChatOpenId = reader["WeChatOpenId"].ToString();
            else
                model.WeChatOpenId = "";

            if (reader["Imgs"] != null)
                model.Imgs = reader["Imgs"].ToString();
            else
                model.Imgs = "";

            return model;
        }

        #endregion


        /// <summary>
        /// 创建产品售后反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ProductFeedbacksInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateProductFeedbacks(model);
        }

        /// <summary>
        /// 删除产品售后反馈
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static bool Delete(string idlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteProductFeedbacksById(idlist);
        }

        /// <summary>
        /// 更新产品售后反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductFeedbacksInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateProductFeedbacks(model);
        }

        /// <summary>
        /// 获得产品售后反馈
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static ProductFeedbacksInfo GetModelById(int id)
        {
            ProductFeedbacksInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductFeedbacksInfoById(id);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();
            return model;
        }



        public static DataTable AdminGetProductFeedbacksList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductFeedbacksList(pageSize, pageNumber, condition, sort);
        }

        public static string AdminGetProductFeedbacksCondition(string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductFeedbacksListCondition(title);
        }

        public static string AdminGetProductFeedbacksListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductFeedbacksListSort(sortColumn, sortDirection);
        }

        public static int AdminGetProductFeedbacksCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductFeedbacksCount(condition);
        }
    }
}
