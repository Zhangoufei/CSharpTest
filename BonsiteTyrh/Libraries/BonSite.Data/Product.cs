using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class Product
    {

        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ProductInfo
        /// </summary>
        public static ProductInfo BuildFromReader(IDataReader reader)
        {
            ProductInfo model = new ProductInfo();

            model.ProductID = TypeHelper.ObjectToInt(reader["ProductID"]);
            model.ProductClassID = TypeHelper.ObjectToInt(reader["ProductClassID"]);
            model.IsShow = TypeHelper.ObjectToInt(reader["IsShow"]);
            model.IsTop = TypeHelper.ObjectToInt(reader["IsTop"]);
            model.IsBest = TypeHelper.ObjectToInt(reader["IsBest"]);
            model.Title = reader["Title"].ToString();
            if (reader["Body"] != null)
                model.Body = reader["Body"].ToString();
            else
                model.Body = "";
            model.AddTime = TypeHelper.ObjectToDateTime(reader["AddTime"]);
            model.UpdateTime = TypeHelper.ObjectToDateTime(reader["UpdateTime"]);
            model.ImgUrl = reader["ImgUrl"].ToString();
            model.BigImgUrl = reader["BigImgUrl"].ToString();
            model.Digest = reader["Digest"].ToString();
            if (reader["Keys"] != null)
                model.Keys = reader["Keys"].ToString();
            else
                model.Keys = "";
            model.AdminID = TypeHelper.ObjectToInt(reader["AdminID"]);
            model.Hits = TypeHelper.ObjectToInt(reader["Hits"]);
            model.Code = reader["Code"].ToString();
            model.Type = reader["type"].ToString();
            model.Provider = reader["Provider"].ToString();
            model.DisplayOrder = TypeHelper.ObjectToInt(reader["DisplayOrder"].ToString());


            if (reader["Keyword"] != null)
                model.Keyword = reader["Keyword"].ToString();
            else
                model.Keyword = "";

            if (reader["Description"] != null)
                model.Description = reader["Description"].ToString();
            else
                model.Description = "";

            return model;
        }

        #endregion


        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ProductInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateProduct(model);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static bool Delete(string productidlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteProductById(productidlist);
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateProduct(model);
        }

        /// <summary>
        /// 获得产品
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static ProductInfo GetModelById(int productid)
        {
            ProductInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductById(productid);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();
            return model;
        }

        /// <summary>
        /// 后台获得产品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得产品列表搜索条件
        /// </summary>
        /// <param name="productClassID">产品类型id</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static string AdminGetProductListCondition(int productClassID, string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductListCondition(productClassID, title);
        }

        /// <summary>
        /// 后台获得产品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得产品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetProductCount(condition);
        }


        /// <summary>
        /// 获得产品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.GetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 获得产品列表搜索条件
        /// </summary>
        /// <param name="productClassID">产品类型id</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static string GetProductListCondition(int productClassID, string title)
        {
            return BonSite.Core.BSData.RDBS.GetProductListCondition(productClassID, title);
        }

        /// <summary>
        /// 获得产品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetProductListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.GetProductListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得产品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetProductCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.GetProductCount(condition);
        }






        /// <summary>
        /// 前台获取置顶文件列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetTopProductList(int productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetTopProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取置顶文件列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetTopProductList(string productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetTopProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取推荐产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetBestProductList(int productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetBestProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取推荐产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetBestProductList(string productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetBestProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取最新产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetProductList(int productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取最新产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ProductInfo> GetProductList(string productClassId, int count, string sortColumn)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductList(productClassId, count, sortColumn);
            while (reader.Read())
            {
                ProductInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        
    }
}
