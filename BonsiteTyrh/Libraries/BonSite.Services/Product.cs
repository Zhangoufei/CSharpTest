using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class Product
    {

        #region 读写删
        /// <summary>
        /// 新加产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ProductInfo model)
        {
            return BonSite.Data.Product.Create(model);
        }

        /// <summary>
        /// 获取产品内容
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static ProductInfo GetModelByProductID(int productid)
        {
            if (productid > 0)
                return BonSite.Data.Product.GetModelById(productid);
            return null;
        }

        /// <summary>
        /// 更新产品内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductInfo model)
        {
            return BonSite.Data.Product.Update(model);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.Product.Delete(CommonHelper.IntArrayToString(idlist));
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ARTICLECLASS_LIST + "\\d+");
                return isSucces;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 后台
        /// <summary>
        /// 后台获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Product.AdminGetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductCount(string condition)
        {
            return BonSite.Data.Product.AdminGetProductCount(condition);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="productClassID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string AdminGetProductListCondition(int productClassID, string title)
        {
            return BonSite.Data.Product.AdminGetProductListCondition(productClassID, title);
        }

        /// <summary>
        /// 后台获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Product.AdminGetProductListSort(sortColumn, sortDirection);
        }
        #endregion

        #region 前台
        /// <summary>
        /// 获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Product.GetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetProductCount(string condition)
        {
            return BonSite.Data.Product.GetProductCount(condition);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="productClassID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetProductListCondition(int productClassID, string title)
        {
            return BonSite.Data.Product.GetProductListCondition(productClassID, title);
        }

        /// <summary>
        /// 获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetProductListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Product.GetProductListSort(sortColumn, sortDirection);
        }
        #endregion

        public static List<ProductInfo> GetTopProductList(int productClassId, int count)
        {
            return BonSite.Data.Product.GetTopProductList(productClassId, count, "");
        }

        public static List<ProductInfo> GetTopProductList(string productClassId, int count)
        {
            return BonSite.Data.Product.GetTopProductList(productClassId, count, "");
        }

        public static List<ProductInfo> GetBestProductList(int productClassId, int count)
        {
            return BonSite.Data.Product.GetBestProductList(productClassId, count, "");
        }

        public static List<ProductInfo> GetBestProductList(string productClassId, int count)
        {
            return BonSite.Data.Product.GetBestProductList(productClassId, count, "");
        }

        public static List<ProductInfo> GetProductList(int productClassId, int count)
        {
            return BonSite.Data.Product.GetProductList(productClassId, count, "");
        }

        public static List<ProductInfo> GetProductList(string productClassId, int count)
        {
            return BonSite.Data.Product.GetProductList(productClassId, count, "");
        }



    }
}
