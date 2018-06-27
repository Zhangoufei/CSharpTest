using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Services
{
    public class ProductFeedBacks
    {

        #region 读写删
        /// <summary>
        /// 新加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ProductFeedbacksInfo model)
        {
            return BonSite.Data.ProductFeedBacks.Create(model);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static ProductFeedbacksInfo GetModelByID(int id)
        {
            if (id > 0)
                return BonSite.Data.ProductFeedBacks.GetModelById(id);
            return null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductFeedbacksInfo model)
        {
            return BonSite.Data.ProductFeedBacks.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.ProductFeedBacks.Delete(CommonHelper.IntArrayToString(idlist));
                return isSucces;
            }
            else
            {
                return false;
            }
        }
        #endregion




        public static DataTable AdminGetProductFeedbacksList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.ProductFeedBacks.AdminGetProductFeedbacksList(pageSize, pageNumber, condition, sort);
        }


        public static int AdminGetProductFeedbacksCount(string condition)
        {
            return BonSite.Data.ProductFeedBacks.AdminGetProductFeedbacksCount(condition);
        }

        public static string AdminGetProductFeedbacksListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.ProductFeedBacks.AdminGetProductFeedbacksListSort(sortColumn, sortDirection);
        }

        public static string AdminGetProductFeedbacksCondition(string condition)
        {
            return BonSite.Data.ProductFeedBacks.AdminGetProductFeedbacksCondition(condition);
        }
    }
}
